import { Injectable, inject, signal, computed } from '@angular/core';
import { forkJoin, map, Observable, switchMap } from 'rxjs';
import { HydratedProjectRecord, ProjectRecord, ProjectRecordTag } from '../../../model/project-record.model';
import { ProjectRecordTagService } from './project-record-tag.service';
import { PersistenceService } from '../../persistence/persistence.service';

@Injectable({ providedIn: 'root' })
export class ProjectRecordService {

  private persistence = inject(PersistenceService);
  private projectTagService = inject(ProjectRecordTagService);

  private readonly file = 'project-records.json';

  // State
  private readonly _currentProject = signal<HydratedProjectRecord | null>(null);
  readonly currentProject = this._currentProject.asReadonly();
  readonly hasProject = computed(() => this._currentProject() !== null);
  readonly projectId = computed(() => this._currentProject()?.id ?? null);
  readonly projectName = computed(() => this._currentProject()?.title ?? null);

  // State actions
  setProject(project: HydratedProjectRecord): void {
    this._currentProject.set(project);
  }

  updateCurrentProject(partial: Partial<HydratedProjectRecord>): void {
    const current = this._currentProject();
    if (!current) return;
    const updated = { ...current, ...partial, updatedAt: new Date() };
    this._currentProject.set(updated);
    this.save(updated).subscribe();
  }

  clearProject(): void {
    this._currentProject.set(null);
  }

  // Queries
  getAll(): Observable<HydratedProjectRecord[]> {
    return forkJoin({
      projects: this.persistence.read<ProjectRecord[]>(this.file),
      tags: this.projectTagService.getAll()
    }).pipe(
      map(({ projects, tags }) =>
        projects.map(p => ({
          ...p,
          createdAt: new Date(p.createdAt),
          updatedAt: new Date(p.updatedAt),
          lastOpenedAt: p.lastOpenedAt ? new Date(p.lastOpenedAt) : undefined,
          tags: p.tags.map(id => tags.find(t => t.id === id)).filter((t): t is ProjectRecordTag => !!t)
        }))
      )
    );
  }

  getFavorites(): Observable<HydratedProjectRecord[]> {
    return this.getAll().pipe(
      map(projects => projects.filter(p => p.isFavorite))
    );
  }

  getById(id: string): Observable<HydratedProjectRecord | undefined> {
    return this.getAll().pipe(
      map(projects => projects.find(p => p.id === id))
    );
  }

  // Persistence
  save(project: HydratedProjectRecord): Observable<void> {
    return this.persistence.read<ProjectRecord[]>(this.file).pipe(
      map(projects => {
        const index = projects.findIndex(p => p.id === project.id);
        const serialized = this.serialize(project);
        if (index !== -1) {
          projects[index] = serialized;
        } else {
          projects.push(serialized);
        }
        return projects;
      }),
      switchMap(projects => this.persistence.write(this.file, projects))
    );
  }

  delete(id: string): Observable<void> {
    return this.persistence.read<ProjectRecord[]>(this.file).pipe(
      map(projects => projects.filter(p => p.id !== id)),
      switchMap(projects => this.persistence.write(this.file, projects))
    );
  }

  private serialize(project: HydratedProjectRecord): ProjectRecord {
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