import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsorshipTypeList } from './sponsorship-type-list';

describe('SponsorshipTypeList', () => {
  let component: SponsorshipTypeList;
  let fixture: ComponentFixture<SponsorshipTypeList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SponsorshipTypeList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SponsorshipTypeList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
