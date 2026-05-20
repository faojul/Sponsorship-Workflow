import {
  ChangeDetectorRef,
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
import { MatSnackBar } from '@angular/material/snack-bar';

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
    'amount',
    'EventName',
    'EventDate',
    'sponsorshipTypeName',
    'actions'
  ];

  constructor(
    private requestService:
      SponsorshipRequestService,
      private cdr: ChangeDetectorRef,
      private snackBar: MatSnackBar
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
        this.cdr.detectChanges();
      });
  }

  approve(id: string): void {

    this.requestService
      .managerApprove(id, 'Approved By Manager')
      .subscribe({

        next: () => {
        // Success case
        this.snackBar.open('Approved successfully.', 'Close', { duration: 3000 });
        this.load();
      },
      error: (err) => {
        // Error case: Safeguard against missing array properties or network failures
        const errorMessages = err?.error?.messages || err?.messages;
        const displayMessage = errorMessages && errorMessages.length > 0 
          ? errorMessages[0] 
          : 'An unexpected error occurred.';

        // 2. Show the backend string directly inside the toast
        this.snackBar.open(displayMessage, 'Close', {
          duration: 5000,
          panelClass: ['error-snackbar'] // Optional styling class
        });
      }
      });
  }

  reject(id: string): void {

    this.requestService
      .managerReject(id, 'Rejected By Manager')
      .subscribe({

        next: () => {
        // Success case
        this.snackBar.open('Request rejected successfully.', 'Close', { duration: 3000 });
        this.load();
      },
      error: (err) => {
        // Error case: Safeguard against missing array properties or network failures
        const errorMessages = err?.error?.messages || err?.messages;
        const displayMessage = errorMessages && errorMessages.length > 0 
          ? errorMessages[0] 
          : 'An unexpected error occurred.';

        // 2. Show the backend string directly inside the toast
        this.snackBar.open(displayMessage, 'Close', {
          duration: 5000,
          panelClass: ['error-snackbar'] // Optional styling class
        });
      }
      });
  }
}
