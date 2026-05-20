export interface CreateSponsorshipRequest {
  title: string;
  department: string;
  sponsorshipTypeId: string;
  eventName: string;
  eventDate: string;
  requestedAmount: number;
  purpose: string;
  expectedBusinessBenefit?: string;
  remarks?: string;
}