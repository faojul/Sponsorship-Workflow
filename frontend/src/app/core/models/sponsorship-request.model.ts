export interface SponsorshipRequest {
  id: string;
  title: string;
  department: string;
  sponsorshipTypeName: string;
  eventName: string;
  eventDate: string;
  requestedAmount: number;
  purpose: string;
  status: string;
  createdAtUtc: string;
}