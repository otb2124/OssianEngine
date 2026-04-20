import { Injectable, inject, signal, computed } from '@angular/core';
import { Observable, catchError, filter, switchMap, take, tap } from 'rxjs';
import { PersistenceService } from '../../persistence/persistence.service';
import { HydratedProjectRecord } from '../../../model/project-record.model';
import { ProjectRecordService } from '../project-record/project-record.service';
import { toObservable } from '@angular/core/rxjs-interop';
import { ProjectConfig } from '../../../model/project-config.model';


@Injectable({ providedIn: 'root' })
export class ProjectConfigService {

  private persistence = inject(PersistenceService);
  private projectRecordService = inject(ProjectRecordService);

  private readonly currentProject$ = toObservable(this.projectRecordService.currentProject);

  private readonly configFileName = '.ossian.project.json';

  // State
  private readonly _config = signal<ProjectConfig | null>(null);
  readonly config = this._config.asReadonly();
  readonly hasConfig = computed(() => this._config() !== null);
  readonly projectId = computed(() => this._config()?.projectId ?? null);

  getOrCreate(project: HydratedProjectRecord): Observable<ProjectConfig> {
    const path = `${project.directoryPath}/${this.configFileName}`;

    return this.persistence.readAbsolute<ProjectConfig>(path).pipe(
      catchError(() => {
        const config: ProjectConfig = { projectId: project.id, targetDirectory: '/target', resDirectory: '/res' };
        return this.persistence.writeAbsolute(path, config).pipe(
          switchMap(() => [config])
        );
      }),
      tap(config => this._config.set(config))
    );
  }

  getOrCreateFromCurrent(): Observable<ProjectConfig> {
    return this.currentProject$.pipe(
      filter(project => !!project),
      take(1),
      switchMap(project => this.getOrCreate(project!))
    );
  }

  updateConfig(project: HydratedProjectRecord, partial: Partial<ProjectConfig>): Observable<void> {
    const current = this._config();
    if (!current) throw new Error('No config loaded');
    const updated = { ...current, ...partial };
    const path = `${project.directoryPath}/${this.configFileName}`;
    return this.persistence.writeAbsolute(path, updated).pipe(
      tap(() => this._config.set(updated))
    );
  }

  updateCurrentConfig(partial: Partial<ProjectConfig>): Observable<void> {
    return this.currentProject$.pipe(
      filter(project => !!project),
      take(1),
      switchMap(project => this.updateConfig(project!, partial))
    );
  }

  clearConfig(): void {
    this._config.set(null);
  }
}