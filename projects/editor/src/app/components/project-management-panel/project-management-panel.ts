import { Component, inject, output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { invoke } from '@tauri-apps/api/core';
import { switchMap, filter, catchError, EMPTY, forkJoin, of } from 'rxjs';
import { DialogService } from '../../services/persistence/dialog.service';
import { ProjectConfigService } from '../../services/projects/project-config/project-config.service';
import { ProjectRecordService } from '../../services/projects/project-record/project-record.service';
import { DialogWrapper } from '../dialog-wrapper/dialog-wrapper';
import { ProjectRecordForm } from '../project-record-form/project-record-form';
import { HydratedProjectRecord } from '../../model/project-record.model';

@Component({
  selector: 'app-project-management-panel',
  imports: [ButtonModule, DialogWrapper, ProjectRecordForm],
  templateUrl: './project-management-panel.html',
})
export class ProjectManagementPanel {

  private dialog = inject(DialogService);
  private projectService = inject(ProjectRecordService);
  private projectConfigService = inject(ProjectConfigService);

  readonly projectsChanged = output<void>();

  showCreateDialog = false;

  openCreateDialog(): void {
    this.showCreateDialog = true;
  }

  onCreateSubmitted(partial: Partial<HydratedProjectRecord>): void {
    const newProject: any = {
      id: crypto.randomUUID(),
      isFavorite: false,
      createdAt: new Date(),
      updatedAt: new Date(),
      tags: [],
      ...partial,
    };
    this.projectConfigService.getOrCreate(newProject).pipe(
      switchMap(() => this.projectService.save(newProject)),
      catchError(err => { console.error('createProject error:', err); return EMPTY; })
    ).subscribe(() => {
      this.showCreateDialog = false;
      this.projectsChanged.emit();
    });
  }

  importProject(): void {
    this.dialog.pickFolder().pipe(
      filter(folder => !!folder),
      switchMap(folder => {
        const name = folder!.split(/[\\/]/).pop() ?? 'Imported Project';
        const newProject = {
          id: crypto.randomUUID(),
          title: name,
          directoryPath: folder!,
          isFavorite: false,
          createdAt: new Date(),
          updatedAt: new Date(),
          tags: [],
        };
        return this.projectConfigService.getOrCreate(newProject as any).pipe(
          switchMap(config => {
            const projectWithConfig = { ...newProject, id: config.projectId };
            return this.projectService.save(projectWithConfig as any);
          })
        );
      }),
      catchError(err => { console.error('importProject error:', err); return EMPTY; })
    ).subscribe(() => this.projectsChanged.emit());
  }

  scanForProjects(): void {
    this.dialog.pickFolder().pipe(
      filter(folder => !!folder),
      switchMap(folder => invoke<string[]>('scan_for_projects', { root: folder! })),
      switchMap(paths => {
        if (!paths.length) return EMPTY;
        const saves = paths.map(folder => {
          const name = folder.split(/[\\/]/).pop() ?? 'Scanned Project';
          const newProject = {
            id: crypto.randomUUID(),
            title: name,
            directoryPath: folder,
            isFavorite: false,
            createdAt: new Date(),
            updatedAt: new Date(),
            tags: [],
          };
          return this.projectConfigService.getOrCreate(newProject as any).pipe(
            switchMap(config => {
              const projectWithConfig = { ...newProject, id: config.projectId };
              return this.projectService.save(projectWithConfig as any);
            })
          );
        });
        return forkJoin(saves);
      }),
      catchError(err => { console.error('scanForProjects error:', err); return EMPTY; })
    ).subscribe(() => this.projectsChanged.emit());
  }
}