import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkflowHistory } from './workflow-history';

describe('WorkflowHistory', () => {
  let component: WorkflowHistory;
  let fixture: ComponentFixture<WorkflowHistory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkflowHistory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkflowHistory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
