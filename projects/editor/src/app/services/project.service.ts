import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, map } from 'rxjs';
import { HydratedProject, Project, ProjectTag } from '../model/project.model';
import { ProjectTagService } from './project-tag.service';

@Injectable({ providedIn: 'root' })
export class ProjectService {

  private readonly path = 'assets/config/projects.json';

  constructor(private http: HttpClient, private projectTagService: ProjectTagService) {}

  getAll(): Observable<HydratedProject[]> {
    return forkJoin({
      projects: this.http.get<Project[]>(this.path).pipe(
        map(projects => projects.map(p => ({
          ...p,
          createdAt: new Date(p.createdAt),
          updatedAt: new Date(p.updatedAt),
          lastOpenedAt: p.lastOpenedAt ? new Date(p.lastOpenedAt) : undefined
        })))
      ),
      tags: this.projectTagService.getAll()
    }).pipe(
      map(({ projects, tags }) =>
        projects.map(p => ({
          ...p,
          tags: p.tags.map(id => tags.find(t => t.id === id)).filter((t): t is ProjectTag => !!t)
        }))
      )
    );
  }

  getFavorites(): Observable<HydratedProject[]> {
    return this.getAll().pipe(
      map(projects => projects.filter(p => p.isFavorite))
    );
  }

  getById(id: string): Observable<HydratedProject | undefined> {
    return this.getAll().pipe(
      map(projects => projects.find(p => p.id === id))
    );
  }
}