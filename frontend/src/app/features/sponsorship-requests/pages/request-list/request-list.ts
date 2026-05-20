import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';

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
      private cdr: ChangeDetectorRef
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
    .subscribe(() => {

      this.loadRequests();
    });

    
}

cancel(id: string): void {

  this.requestService
    .cancel(id)
    .subscribe(() => {

      this.loadRequests();
    });
}
}
