import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabsControl } from '../tabs-control/tabs-control';
import { ModuleControl } from "../module-control/module-control";
import { ProjectRecordService } from '../../services/projects/project-record/project-record.service';
import { UrlControl } from "../url-control/url-control";
import { AppTitle } from "../app-title/app-title";


@Component({
  selector: 'app-topbar',
  imports: [CommonModule, TabsControl, ModuleControl, UrlControl, AppTitle],
  templateUrl: './topbar.html',
})
export class Topbar {

  protected projectService = inject(ProjectRecordService);
  
  
}