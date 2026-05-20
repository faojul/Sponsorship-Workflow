import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { SponsorshipRequest } from '../../../core/models/sponsorship-request.model';

@Injectable({
  providedIn: 'root',
})
export class SponsorshipRequestService  {
  private apiUrl =
    `${environment.apiUrl}/SponsorshipRequests`;

  constructor(
    private http: HttpClient
  ) {

  }

  getMine():
    Observable<SponsorshipRequest[]> {

    return this.http.get<SponsorshipRequest[]>(
      `${this.apiUrl}/mine`
    );
  }

  getAll():
    Observable<SponsorshipRequest[]> {

    return this.http.get<SponsorshipRequest[]>(
      this.apiUrl
    );
  }

  createDraft(request: any) {

    return this.http.post(
      `${this.apiUrl}/draft`,
      request
    );
  }

  submit(id: string) {

    return this.http.post(
      `${this.apiUrl}/${id}/submit`,
      {}
    );
  }

  managerApprove(
    id: string,
    remarks: string
  ) {

    return this.http.post(
      `${this.apiUrl}/${id}/manager-approve`,
      { remarks }
    );
  }

  managerReject(
    id: string,
    remarks: string
  ) {

    return this.http.post(
      `${this.apiUrl}/${id}/manager-reject`,
      { remarks }
    );
  }

  financeApprove(
    id: string,
    remarks: string
  ) {

    return this.http.post(
      `${this.apiUrl}/${id}/finance-approve`,
      { remarks }
    );
  }

  financeReject(
    id: string,
    remarks: string
  ) {

    return this.http.post(
      `${this.apiUrl}/${id}/finance-reject`,
      { remarks }
    );
  }

  cancel(id: string) {

  return this.http.post(
    `${this.apiUrl}/${id}/cancel`,
    {}
  );
}

getHistory(id: string) {

  return this.http.get(
    `${this.apiUrl}/${id}/history`
  );
}
getPendingManagerApprovals() {

  return this.http.get(
    `${this.apiUrl}?status=PendingManagerApproval`
  );
}
getPendingFinanceApprovals() {

  return this.http.get(
    `${this.apiUrl}?status=PendingFinanceReview`
  );
}
}
