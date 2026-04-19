import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccordionModule } from 'primeng/accordion';
import { ProjectService, } from '../../services/project.service';
import { HydratedProject } from '../../model/project.model';
import { ProjectOverview } from '../project-overview/project-overview';
import { ProjectDetails } from '../project-details/project-details';

@Component({
  selector: 'app-project-accordion',
  imports: [CommonModule, AccordionModule, ProjectOverview, ProjectDetails],
  templateUrl: './project-accordion.html',
})
export class ProjectAccordion implements OnInit {

  projects: HydratedProject[] = [];

  constructor(private projectService: ProjectService) {}

  ngOnInit() {
    this.projectService.getAll().subscribe(p => this.projects = p);
  }

  openInExplorer(path: string) {
    console.log('open', path);
  }
}