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
  RouterModule
}
from '@angular/router';

import {
  MatTableModule
}
from '@angular/material/table';

import {
  MatButtonModule
}
from '@angular/material/button';

import {
  MatCardModule
}
from '@angular/material/card';

import {
  SponsorshipRequestService
}
from '../../../sponsorship-requests/services/sponsorship-request';

@Component({
  selector: 'app-all-requests',
  imports: [    CommonModule,
    RouterModule,
    MatTableModule,
    MatButtonModule,
    MatCardModule],
  templateUrl: './all-requests.html',
  styleUrl: './all-requests.scss',
})
export class AllRequests implements OnInit {

  requests: any[] = [];

  displayedColumns = [
    'title',
    'department',
    'status',
    'amount',
    'workflow'
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
      .getAll()
      .subscribe(response => {

        this.requests =
          (response as any).data ??
          response;
      });
  }
}
