import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerApprovalList } from './manager-approval-list';

describe('ManagerApprovalList', () => {
  let component: ManagerApprovalList;
  let fixture: ComponentFixture<ManagerApprovalList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManagerApprovalList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManagerApprovalList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
