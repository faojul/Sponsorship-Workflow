import {
  ChangeDetectorRef,
  Component,
  OnInit
}
from '@angular/core';

import {
  ActivatedRoute
}
from '@angular/router';

import {
  CommonModule
}
from '@angular/common';

import {
  SponsorshipRequestService
}
from '../../../sponsorship-requests/services/sponsorship-request';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-workflow-history',
  imports: [   CommonModule,
    MatCardModule],
  templateUrl: './workflow-history.html',
  styleUrl: './workflow-history.scss',
})
export class WorkflowHistory implements OnInit {

  history: any[] = [];

  constructor(
    private route:
      ActivatedRoute,

    private requestService:
      SponsorshipRequestService,
      private cdr: ChangeDetectorRef
  ) {

  }

  ngOnInit(): void {

    const id =
      this.route.snapshot.paramMap.get('id');

    this.requestService
      .getHistory(id!)
      .subscribe(response => {

        this.history =
          (response as any).data ??
          response;
          this.cdr.detectChanges();
      });
  }
}
