import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { HydratedProject } from '../../model/project.model';

@Component({
  selector: 'app-project-details',
  imports: [CommonModule, TagModule, ButtonModule],
  templateUrl: './project-details.html',
})
export class ProjectDetails {
  @Input({ required: true }) project!: HydratedProject;
  @Output() openFolder = new EventEmitter<string>();

  openInExplorer() {
    this.openFolder.emit(this.project.directoryPath);
  }
}