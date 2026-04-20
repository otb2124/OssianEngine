import { Component, inject } from '@angular/core';
import { ProjectConfigService } from '../../../services/projects/project-config/project-config.service';

@Component({
  selector: 'app-project-overview',
  imports: [],
  templateUrl: './project-overview.html',
  styleUrl: './project-overview.css',
})
export class ProjectOverview {
  protected configService = inject(ProjectConfigService);
}
