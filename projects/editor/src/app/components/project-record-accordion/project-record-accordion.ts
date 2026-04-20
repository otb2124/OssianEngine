import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccordionModule } from 'primeng/accordion';
import { ProjectRecordService } from '../../services/projects/project-record/project-record.service';
import { HydratedProjectRecord } from '../../model/project-record.model';
import { ProjectRecordOverview } from '../project-record-overview/project-record-overview';
import { ProjectRecordDetails } from '../project-record-details/project-record-details';

@Component({
  selector: 'app-project-record-accordion',
  imports: [CommonModule, AccordionModule, ProjectRecordOverview, ProjectRecordDetails],
  templateUrl: './project-record-accordion.html',
})
export class ProjectRecordAccordion implements OnInit {

  projects: HydratedProjectRecord[] = [];

  constructor(private projectService: ProjectRecordService) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.projectService.getAll().subscribe(p => this.projects = p);
  }

  onDeleted(projectId: string): void {
    this.projects = this.projects.filter(p => p.id !== projectId);
  }
}