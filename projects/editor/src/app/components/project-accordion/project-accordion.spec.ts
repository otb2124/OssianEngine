import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectAccordion } from './project-accordion';

describe('ProjectAccordion', () => {
  let component: ProjectAccordion;
  let fixture: ComponentFixture<ProjectAccordion>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProjectAccordion]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProjectAccordion);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
