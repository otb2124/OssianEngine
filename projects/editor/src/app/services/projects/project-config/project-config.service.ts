import { Injectable, inject, signal, computed } from '@angular/core';
import { Observable, catchError, switchMap, tap } from 'rxjs';
import { PersistenceService } from '../../persistence/persistence.service';
import { HydratedProjectRecord } from '../../../model/project-record.model';

export interface ProjectConfig {
  projectId: string;
}

@Injectable({ providedIn: 'root' })
export class ProjectConfigService {

  private persistence = inject(PersistenceService);
  private readonly configFileName = 'config.json';

  // State
  private readonly _config = signal<ProjectConfig | null>(null);
  readonly config = this._config.asReadonly();
  readonly hasConfig = computed(() => this._config() !== null);
  readonly projectId = computed(() => this._config()?.projectId ?? null);

  getOrCreate(project: HydratedProjectRecord): Observable<ProjectConfig> {
    const path = `${project.directoryPath}/${this.configFileName}`;

    return this.persistence.readAbsolute<ProjectConfig>(path).pipe(
      catchError(() => {
        const config: ProjectConfig = { projectId: project.id };
        return this.persistence.writeAbsolute(path, config).pipe(
          switchMap(() => [config])
        );
      }),
      tap(config => this._config.set(config))
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

  clearConfig(): void {
    this._config.set(null);
  }
}