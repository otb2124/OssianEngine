import { Component, Input, inject, computed, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { HydratedProjectRecord } from '../../model/project-record.model';
import { Router } from '@angular/router';
import { invoke } from '@tauri-apps/api/core';
import { ProjectRecordService } from '../../services/projects/project-record/project-record.service';
import { DialogWrapper } from '../dialog-wrapper/dialog-wrapper';
import { ProjectRecordForm } from '../project-record-form/project-record-form';
import { ConfirmForm } from '../confirm-form/confirm-form';

@Component({
  selector: 'app-project-record-details',
  imports: [CommonModule, TagModule, ButtonModule, DialogWrapper, ProjectRecordForm, ConfirmForm],
  templateUrl: './project-record-details.html',
})
export class ProjectRecordDetails {
  @Input({ required: true }) project!: HydratedProjectRecord;

  readonly deleted = output<void>();
  readonly updated = output<HydratedProjectRecord>();

  private projectService = inject(ProjectRecordService);
  private router = inject(Router);

  readonly isActive = computed(() => this.projectService.projectId() === this.project.id);

  showEditDialog = false;
  showDeleteDialog = false;

  openProject(event: Event): void {
    event.stopPropagation();
    if (this.isActive()) return;
    this.projectService.setProject(this.project);
    this.router.navigateByUrl('/project');
  }

  closeProject(event: Event): void {
    event.stopPropagation();
    if (!this.isActive()) return;
    this.projectService.clearProject();
    this.router.navigateByUrl('/general');
  }

  async openInExplorer(event: Event): Promise<void> {
    event.stopPropagation();
    await invoke('reveal_in_explorer', { path: this.project.directoryPath });
  }

  openDeleteDialog(event: Event): void {
    event.stopPropagation();
    this.showDeleteDialog = true;
  }

  confirmDelete(): void {
    if (this.isActive()) {
      this.projectService.clearProject();
      this.router.navigateByUrl('/general');
    }
    this.projectService.delete(this.project.id).subscribe(() => this.deleted.emit());
  }

  openEditDialog(event: Event): void {
    event.stopPropagation();
    this.showEditDialog = true;
  }

  onEditSubmitted(partial: Partial<HydratedProjectRecord>): void {
    const updated: HydratedProjectRecord = {
      ...this.project,
      ...partial,
      updatedAt: new Date(),
    };
    this.projectService.save(updated).subscribe(() => {
      this.project = updated;
      if (this.isActive()) {
        this.projectService.setProject(updated);
      }
      this.showEditDialog = false;
      this.updated.emit(updated);
    });
  }
}