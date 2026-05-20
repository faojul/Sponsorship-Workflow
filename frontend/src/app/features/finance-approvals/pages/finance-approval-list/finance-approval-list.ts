import {
  Component,
  OnInit
}
from '@angular/core';

import {
  CommonModule
}
from '@angular/common';

import {
  MatTableModule
}
from '@angular/material/table';

import {
  MatButtonModule
}
from '@angular/material/button';

import {
  SponsorshipRequestService
}
from '../../../sponsorship-requests/services/sponsorship-request';

@Component({
  selector: 'app-finance-approval-list',
  imports: [
        CommonModule,
    MatTableModule,
    MatButtonModule
  ],
  templateUrl: './finance-approval-list.html',
  styleUrl: './finance-approval-list.scss',
})
export class FinanceApprovalList implements OnInit {

  requests: any[] = [];

  displayedColumns = [
    'title',
    'department',
    'actions'
  ];

  constructor(
    private requestService:
      SponsorshipRequestService
  ) {

  }

  ngOnInit(): void {

    this.load();
  }

  load(): void {

    this.requestService
      .getPendingFinanceApprovals()
      .subscribe(response => {

        this.requests =
          (response as any).data ??
          response;
      });
  }

  approve(id: string): void {

    this.requestService
      .financeApprove(id, 'Approved')
      .subscribe(() => {

        this.load();
      });
  }

  reject(id: string): void {

    this.requestService
      .financeReject(id, 'Rejected')
      .subscribe(() => {

        this.load();
      });
  }
}
