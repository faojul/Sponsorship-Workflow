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
  selector: 'app-manager-approval-list',
  imports: [
        CommonModule,
    MatTableModule,
    MatButtonModule
  ],
  templateUrl: './manager-approval-list.html',
  styleUrl: './manager-approval-list.scss',
})
export class ManagerApprovalList implements OnInit {

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
      .getPendingManagerApprovals()
      .subscribe(response => {

        this.requests =
          (response as any).data ??
          response;
      });
  }

  approve(id: string): void {

    this.requestService
      .managerApprove(id, 'Approved')
      .subscribe(() => {

        this.load();
      });
  }

  reject(id: string): void {

    this.requestService
      .managerReject(id, 'Rejected')
      .subscribe(() => {

        this.load();
      });
  }
}
