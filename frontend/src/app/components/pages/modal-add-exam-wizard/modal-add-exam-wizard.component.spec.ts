import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalAddExamWizardComponent } from './modal-add-exam-wizard.component';

describe('ModalAddExamWizardComponent', () => {
  let component: ModalAddExamWizardComponent;
  let fixture: ComponentFixture<ModalAddExamWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalAddExamWizardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalAddExamWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
