import { TestBed } from '@angular/core/testing';

import { SponsorshipRequest } from './sponsorship-request';

describe('SponsorshipRequest', () => {
  let service: SponsorshipRequest;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SponsorshipRequest);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
