import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TabsControl } from '../tabs-control/tabs-control';
import { ModuleControl } from "../module-control/module-control";
import { ProjectRecordService } from '../../services/projects/project-record/project-record.service';
import { UrlControl } from "../url-control/url-control";
import { AppTitle } from "../app-title/app-title";
import { ProjectUiService } from '../../services/projects/project-ui/project-ui.service';

@Component({
  selector: 'app-topbar',
  imports: [CommonModule, TabsControl, ModuleControl, UrlControl, AppTitle],
  templateUrl: './topbar.html',
})
export class Topbar {
  protected projectService = inject(ProjectRecordService);
  private projectUiService = inject(ProjectUiService);
  private router = inject(Router);

  goToProject(): void {
    const id = this.projectService.projectId();
    if (!id) return;
    this.projectUiService.scrollTo(id);
    this.router.navigateByUrl('/general/home');
  }
}