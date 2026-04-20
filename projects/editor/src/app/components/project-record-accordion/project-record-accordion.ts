import { Component, OnInit, signal, inject, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccordionModule } from 'primeng/accordion';
import { ProjectRecordService } from '../../services/projects/project-record/project-record.service';
import { HydratedProjectRecord } from '../../model/project-record.model';
import { ProjectRecordOverview } from '../project-record-overview/project-record-overview';
import { ProjectRecordDetails } from '../project-record-details/project-record-details';
import { ProjectUiService } from '../../services/projects/project-ui/project-ui.service';

@Component({
  selector: 'app-project-record-accordion',
  imports: [CommonModule, AccordionModule, ProjectRecordOverview, ProjectRecordDetails],
  templateUrl: './project-record-accordion.html',
})
export class ProjectRecordAccordion implements OnInit {

  projects: HydratedProjectRecord[] = [];
  activeValues = signal<string[]>([]);

  private projectService = inject(ProjectRecordService);
  private projectUiService = inject(ProjectUiService);

  constructor() {
    effect(() => {
      const id = this.projectUiService.scrollToProjectId();
      if (!id) return;
      this.activeValues.set([id]);
      setTimeout(() => {
        document.getElementById(`panel-${id}`)?.scrollIntoView({ behavior: 'smooth', block: 'start' });
        this.projectUiService.clear();
      }, 50);
    });
  }

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