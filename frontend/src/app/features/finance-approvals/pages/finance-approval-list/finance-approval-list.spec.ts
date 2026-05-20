import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinanceApprovalList } from './finance-approval-list';

describe('FinanceApprovalList', () => {
  let component: FinanceApprovalList;
  let fixture: ComponentFixture<FinanceApprovalList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinanceApprovalList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FinanceApprovalList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
