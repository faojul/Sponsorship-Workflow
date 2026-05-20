import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RouterModule } from '@angular/router';

import { SponsorshipRequestService }
from '../../services/sponsorship-request';


@Component({
  selector: 'app-request-list',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatCardModule,
    RouterModule
  ],
  templateUrl: './request-list.html',
  styleUrl: './request-list.scss',
})
export class RequestList implements OnInit {

  displayedColumns = [
    'title',
    'department',
    'status',
    'amount',
    'actions'
  ];

  requests: any[] = [];

  constructor(
    private requestService:
      SponsorshipRequestService,
      private cdr: ChangeDetectorRef,
      private snackBar: MatSnackBar
  ) {

  }

  ngOnInit(): void {

    this.loadRequests();
  }

  loadRequests(): void {

    this.requestService
      .getMine()
      .subscribe((response: any) => {

        this.requests = response.data;
        this.cdr.detectChanges();
      });
  }

  submit(id: string): void {

  this.requestService
    .submit(id)
    .subscribe({

            next: () => {
        // Success case
        this.snackBar.open('Request submitted successfully.', 'Close', { duration: 3000 });
        this.loadRequests();
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

cancel(id: string): void {
  this.requestService
    .cancel(id)
    .subscribe({
      next: () => {
        // Success case
        this.snackBar.open('Request cancelled successfully.', 'Close', { duration: 3000 });
        this.loadRequests();
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
