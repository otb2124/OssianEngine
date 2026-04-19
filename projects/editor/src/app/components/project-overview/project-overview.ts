import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TagModule } from 'primeng/tag';
import { HydratedProject } from '../../model/project.model';

@Component({
  selector: 'app-project-overview',
  imports: [CommonModule, TagModule],
  templateUrl: './project-overview.html',
})
export class ProjectOverview {
  @Input({ required: true }) project!: HydratedProject;
}