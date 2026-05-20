import { TestBed } from '@angular/core/testing';

import { SponsorshipType } from './sponsorship-type';

describe('SponsorshipType', () => {
  let service: SponsorshipType;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SponsorshipType);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
