import { Component, inject, output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { invoke } from '@tauri-apps/api/core';
import { switchMap, filter, catchError, EMPTY, forkJoin, of } from 'rxjs';
import { DialogService } from '../../services/persistence/dialog.service';
import { DialogWrapper } from '../dialog-wrapper/dialog-wrapper';
import { ProjectRecordForm } from '../project-record-form/project-record-form';
import { HydratedProjectRecord } from '../../model/project-record.model';
import { ProjectService } from '../../services/projects/project.service';

@Component({
  selector: 'app-project-management-panel',
  imports: [ButtonModule, DialogWrapper, ProjectRecordForm],
  templateUrl: './project-management-panel.html',
})
export class ProjectManagementPanel {

  private dialog = inject(DialogService);
  private projectService = inject(ProjectService);

  readonly projectsChanged = output<void>();

  showCreateDialog = false;

  openCreateDialog(): void {
    this.showCreateDialog = true;
  }

  onCreateSubmitted(partial: Partial<HydratedProjectRecord>): void {

    let projectTitle = partial.title;

    const newProject: any = {
      id: crypto.randomUUID(),
      isFavorite: false,
      createdAt: new Date(),
      updatedAt: new Date(),
      tags: [],
      ...partial,
    };
    this.projectService.loadConfig(newProject).pipe(
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
      switchMap(folder => this.projectService.importFromDirectory(folder!)),
      catchError(err => { console.error('importProject error:', err); return EMPTY; })
    ).subscribe(project => {
      this.projectsChanged.emit();
    });
  }
  
  scanForProjects(): void {
    this.dialog.pickFolder().pipe(
      filter(folder => !!folder),
      switchMap(folder => invoke<string[]>('scan_for_projects', { root: folder! })),
      switchMap(paths => {
        if (!paths.length) return EMPTY;
        return forkJoin(paths.map(p => this.projectService.importFromDirectory(p)));
      }),
      catchError(err => { console.error('scanForProjects error:', err); return EMPTY; })
    ).subscribe(projects => {
      this.projectsChanged.emit();
    });
  }
}