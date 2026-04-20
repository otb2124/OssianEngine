import { Injectable, inject } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ProjectRecordTag } from '../../../model/project-record.model';
import { PersistenceService } from '../../persistence/persistence.service';

@Injectable({ providedIn: 'root' })
export class ProjectRecordTagService {

  private persistence = inject(PersistenceService);

  getAll(): Observable<ProjectRecordTag[]> {
    return this.persistence.read<ProjectRecordTag[]>('project-records.json');
  }

  getById(id: string): Observable<ProjectRecordTag | undefined> {
    return this.getAll().pipe(map(tags => tags.find(t => t.id === id)));
  }

  getByLabel(label: string): Observable<ProjectRecordTag | undefined> {
    return this.getAll().pipe(map(tags => tags.find(t => t.label === label)));
  }
}