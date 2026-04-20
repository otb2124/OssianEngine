import { Component, Input, inject, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TagModule } from 'primeng/tag';
import { HydratedProjectRecord } from '../../model/project-record.model';
import { ButtonModule } from "primeng/button";
import { ProjectRecordService } from '../../services/projects/project-record/project-record.service';
import { ProjectConfigService } from '../../services/projects/project-config/project-config.service';

@Component({
  selector: 'app-project-record-overview',
  imports: [CommonModule, TagModule, ButtonModule],
  templateUrl: './project-record-overview.html',
})
export class ProjectRecordOverview {
  @Input({ required: true }) project!: HydratedProjectRecord;

  private projectService = inject(ProjectRecordService);
  private projectConfigService = inject(ProjectConfigService);

  private router = inject(Router);

  readonly isActive = computed(() => this.projectService.projectId() === this.project.id);

  openProject(event: Event): void {
    event.stopPropagation();
    if (this.isActive()) return;
    this.projectService.setProject(this.project);
    this.projectConfigService.getOrCreate(this.project).subscribe();
    this.router.navigateByUrl('/project');
  }

  toggleFavorite(event: Event): void {
    event.stopPropagation();
    const updated = { ...this.project, isFavorite: !this.project.isFavorite };
    this.project = updated;
    this.projectService.save(updated).subscribe();
  }
}