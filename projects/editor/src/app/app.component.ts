import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet} from '@angular/router';
import { CommonModule } from '@angular/common';
import { Topbar } from "./components/topbar/topbar";
import { AppConfigService } from './services/app-config/app-config.service';
import { ProjectRecordService } from './services/projects/project-record/project-record.service';
import { filter, switchMap } from 'rxjs';


@Component({
  selector: 'app-root',
  imports: [
    FormsModule,
    CommonModule,
    RouterOutlet,
    Topbar
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  private appConfigService = inject(AppConfigService);
  private projectRecordService = inject(ProjectRecordService);
  
  ngOnInit(): void {
    this.appConfigService.load().pipe(
      switchMap(config => {
        if (!config.currentProjectId) return [];
        return this.projectRecordService.getById(config.currentProjectId);
      }),
      filter(project => !!project)
    ).subscribe(project => {
      this.projectRecordService.setProject(project!);
    });
  }
}