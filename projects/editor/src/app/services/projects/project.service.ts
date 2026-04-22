import { Injectable, inject, signal, computed } from '@angular/core';
import { forkJoin, map, Observable, switchMap, of, catchError, tap, filter, take } from 'rxjs';
import { toObservable } from '@angular/core/rxjs-interop';
import { PersistenceService } from '../persistence/persistence.service';
import { AppConfigService } from '../app-config/app-config.service';
import { HydratedProjectRecord, ProjectRecord, ProjectRecordTag, ProjectRegistryEntry } from '../../model/project-record.model';
import { ProjectConfig } from '../../model/project-config.model';
import { UserTagService } from '../user-tags/user-tag.service';
import { NotificationService } from '../notifications/notification.service';

@Injectable({ providedIn: 'root' })
export class ProjectService {

  private persistence = inject(PersistenceService);
  private tagService = inject(UserTagService);
  private appConfigService = inject(AppConfigService);
  private notifications = inject(NotificationService);

  private readonly registryFile = 'project-registry.json';
  private readonly configFileName = '.ossian.project.json';

  // ─── Current project state ───────────────────────────────────────────────

  private readonly _currentProject = signal<HydratedProjectRecord | null>(null);
  private readonly _currentConfig = signal<ProjectConfig | null>(null);

  readonly currentProject = this._currentProject.asReadonly();
  readonly currentConfig = this._currentConfig.asReadonly();

  readonly hasProject = computed(() => this._currentProject() !== null);
  readonly projectId = computed(() => this._currentProject()?.id ?? null);
  readonly projectName = computed(() => this._currentProject()?.title ?? null);
  readonly hasConfig = computed(() => this._currentConfig() !== null);
  readonly resDirectory = computed(() => this._currentConfig()?.resDirectory ?? null);
  readonly targetDirectory = computed(() => this._currentConfig()?.targetDirectory ?? null);

  private readonly currentProject$ = toObservable(this._currentProject);

  // ─── State actions ────────────────────────────────────────────────────────

  setProject(project: HydratedProjectRecord): void {
    this._currentProject.set(project);
    this.appConfigService.update({ currentProjectId: project.id }).subscribe();
    this.loadConfig(project).subscribe();
    this.notifications.info(`Project opened`, project.title);
  }

  updateCurrentProject(partial: Partial<HydratedProjectRecord>): void {
    const current = this._currentProject();
    if (!current) return;
    const updated = { ...current, ...partial, updatedAt: new Date() };
    this._currentProject.set(updated);
    this.saveRecord(updated).subscribe();
  }

  clearProject(): void {
    this._currentProject.set(null);
    this._currentConfig.set(null);
    this.appConfigService.update({ currentProjectId: undefined }).subscribe();
    this.notifications.info('Project closed', this._currentProject()?.title);
  }

  // ─── Registry queries ─────────────────────────────────────────────────────

  getAll(): Observable<HydratedProjectRecord[]> {
    return forkJoin({
      entries: this.persistence.read<ProjectRegistryEntry[]>(this.registryFile),
      tags: this.tagService.getAll()
    }).pipe(
      switchMap(({ entries, tags }) => {
        if (!entries.length) return of([]);
        return forkJoin(
          entries.map(entry =>
            this.persistence.readAbsolute<ProjectConfig>(
              `${entry.directoryPath}/${this.configFileName}`
            ).pipe(
              map(config => this.hydrate(config.projectRecord!, tags))
            )
          )
        );
      })
    );
  }

  getFavorites(): Observable<HydratedProjectRecord[]> {
    return this.getAll().pipe(map(projects => projects.filter(p => p.isFavorite)));
  }

  getById(id: string): Observable<HydratedProjectRecord | undefined> {
    return this.getAll().pipe(map(projects => projects.find(p => p.id === id)));
  }

  // ─── Registry persistence ─────────────────────────────────────────────────

  save(project: HydratedProjectRecord): Observable<void> {
    return this.saveRegistry(project).pipe(
      switchMap(() => this.saveRecord(project))
    );
  }

  delete(id: string): Observable<void> {
    return this.persistence.read<ProjectRegistryEntry[]>(this.registryFile).pipe(
      map(entries => entries.filter(e => e.id !== id)),
      switchMap(entries => this.persistence.write(this.registryFile, entries)),
      tap(() => this.notifications.success('Project removed', id))
    );
  }

  // ─── Project config ───────────────────────────────────────────────────────

  loadConfig(project: HydratedProjectRecord): Observable<ProjectConfig> {
    const path = this.configPath(project);
    return this.persistence.readAbsolute<ProjectConfig>(path).pipe(
      catchError(() => {
        const config: ProjectConfig = {
          projectRecord: this.serializeRecord(project),
          resDirectory: '/res',
          targetDirectory: '/target',
        };
        return this.persistence.writeAbsolute(path, config).pipe(
          switchMap(() => [config])
        );
      }),
      tap(config => {
        const synced: ProjectConfig = { ...config, projectRecord: this.serializeRecord(project) };
        this.persistence.writeAbsolute(path, synced).subscribe();
        this._currentConfig.set(synced);
      })
    );
  }

  loadConfigFromCurrent(): Observable<ProjectConfig> {
    return this.currentProject$.pipe(
      filter(p => !!p),
      take(1),
      switchMap(p => this.loadConfig(p!))
    );
  }

  updateConfig(partial: Partial<ProjectConfig>): Observable<void> {
    const project = this._currentProject();
    const config = this._currentConfig();
    if (!project || !config) throw new Error('No current project or config');
    const updated: ProjectConfig = {
      ...config,
      ...partial,
      projectRecord: this.serializeRecord(project),
    };
    return this.persistence.writeAbsolute(this.configPath(project), updated).pipe(
      tap(() => this._currentConfig.set(updated))
    );
  }

  clearConfig(): void {
    this._currentConfig.set(null);
  }

  // ─── Private helpers ──────────────────────────────────────────────────────

  private saveRegistry(project: HydratedProjectRecord): Observable<void> {
    return this.persistence.read<ProjectRegistryEntry[]>(this.registryFile).pipe(
      map(entries => {
        const entry: ProjectRegistryEntry = { id: project.id, directoryPath: project.directoryPath };
        const index = entries.findIndex(e => e.id === project.id);
        if (index !== -1) entries[index] = entry;
        else entries.push(entry);
        return entries;
      }),
      switchMap(entries => this.persistence.write(this.registryFile, entries))
    );
  }

  private saveRecord(project: HydratedProjectRecord): Observable<void> {
    const path = this.configPath(project);
    return this.persistence.readAbsolute<ProjectConfig>(path).pipe(
      catchError(() => of({ projectRecord: undefined, resDirectory: '/res', targetDirectory: '/target' } as ProjectConfig)),
      tap(config => {
        if (config.projectRecord) {
          const changedFields = (Object.keys(project) as (keyof ProjectRecord)[])
            .filter(key => JSON.stringify((config.projectRecord as any)[key]) !== JSON.stringify((this.serializeRecord(project) as any)[key]))
            .map(key => `${key}: ${JSON.stringify((config.projectRecord as any)[key])} → ${JSON.stringify((this.serializeRecord(project) as any)[key])}`);
          if (changedFields.length) {
            this.notifications.info(`${project.title} record saved`, changedFields.join('\n'));
          }
        }
      }),
      map(config => ({ ...config, projectRecord: this.serializeRecord(project) })),
      switchMap(config => this.persistence.writeAbsolute(path, config))
    );
  }

  importFromDirectory(directoryPath: string): Observable<HydratedProjectRecord> {
    const configPath = `${directoryPath}/${this.configFileName}`;
    return this.persistence.readAbsolute<ProjectConfig>(configPath).pipe(
      switchMap(config => {
        if (!config.projectRecord) throw new Error('No project record in config');
        return this.tagService.getAll().pipe(
          map(tags => this.hydrate(config.projectRecord!, tags)),
          switchMap(project => this.saveRegistry(project).pipe(map(() => project))),
          tap(project => this.notifications.success('Project imported', project.title))
        );
      }),
      catchError(() => {
        // No config exists — create a fresh one from the folder name
        const title = directoryPath.split(/[\\/]/).pop() ?? 'Imported Project';
        const newProject: HydratedProjectRecord = {
          id: crypto.randomUUID(),
          title,
          directoryPath,
          isFavorite: false,
          createdAt: new Date(),
          updatedAt: new Date(),
          tags: [],
        };
        return this.saveRecord(newProject).pipe(
          switchMap(() => this.saveRegistry(newProject)),
          map(() => newProject),
          tap(() => this.notifications.success('Project created', title))
        );
      })
    );
  }

  private configPath(project: HydratedProjectRecord): string {
    return `${project.directoryPath}/${this.configFileName}`;
  }

  private hydrate(record: ProjectRecord, tags: ProjectRecordTag[]): HydratedProjectRecord {
    return {
      ...record,
      createdAt: new Date(record.createdAt),
      updatedAt: new Date(record.updatedAt),
      lastOpenedAt: record.lastOpenedAt ? new Date(record.lastOpenedAt) : undefined,
      tags: record.tags.map(id => tags.find(t => t.id === id)).filter((t): t is ProjectRecordTag => !!t),
    };
  }

  private serializeRecord(project: HydratedProjectRecord): ProjectRecord {
    return {
      id: project.id,
      title: project.title,
      description: project.description,
      color: project.color,
      directoryPath: project.directoryPath,
      isFavorite: project.isFavorite,
      createdAt: project.createdAt,
      updatedAt: project.updatedAt,
      lastOpenedAt: project.lastOpenedAt ?? undefined,
      tags: project.tags.map(t => t.id),
    };
  }
}