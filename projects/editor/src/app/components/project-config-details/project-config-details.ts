import { Component, inject, OnInit } from '@angular/core';
import { ProjectConfigService } from '../../services/projects/project-config/project-config.service';

@Component({
  selector: 'app-project-config-details',
  imports: [],
  templateUrl: './project-config-details.html',
  styleUrl: './project-config-details.css',
})
export class ProjectConfigDetails implements OnInit{

  protected configService = inject(ProjectConfigService);

  ngOnInit(): void {
    this.configService.getOrCreateFromCurrent().subscribe();
  }
}
