import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { ProjectTag } from '../model/project.model';

@Injectable({ providedIn: 'root' })
export class ProjectTagService {

  private readonly path = 'assets/config/project-tags.json';

  constructor(private http: HttpClient) {}

  getAll(): Observable<ProjectTag[]> {
    return this.http.get<ProjectTag[]>(this.path);
  }

  getById(id: string): Observable<ProjectTag | undefined> {
    return this.getAll().pipe(
      map(tags => tags.find(t => t.id === id))
    );
  }

  getByLabel(label: string): Observable<ProjectTag | undefined> {
    return this.getAll().pipe(
      map(tags => tags.find(t => t.label === label))
    );
  }
}