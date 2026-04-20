import { TestBed } from '@angular/core/testing';

import { ProjectRecordTagService } from './project-record-tag.service';

describe('ProjectTagService', () => {
  let service: ProjectRecordTagService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectRecordTagService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
