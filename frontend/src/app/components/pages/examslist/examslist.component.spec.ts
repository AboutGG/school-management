import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamslistComponent } from './examslist.component';

describe('ExamslistComponent', () => {
  let component: ExamslistComponent;
  let fixture: ComponentFixture<ExamslistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExamslistComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExamslistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
