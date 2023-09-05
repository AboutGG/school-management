import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamsStudentListComponent } from './exams-student-list.component';

describe('ExamsStudentListComponent', () => {
  let component: ExamsStudentListComponent;
  let fixture: ComponentFixture<ExamsStudentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExamsStudentListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExamsStudentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
