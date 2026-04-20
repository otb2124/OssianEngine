import { TestBed } from '@angular/core/testing';

import { ProjectRecordService } from './project-record.service';

describe('ProjectService', () => {
  let service: ProjectRecordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectRecordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
