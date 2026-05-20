import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';

import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule }
from '@angular/material/datepicker';

import { SponsorshipRequestService }
from '../../services/sponsorship-request';

import { SponsorshipTypeService }
from '../../../sponsorship-types/services/sponsorship-type';
import { AuthService } from '../../../../core/auth/auth';
@Component({
  selector: 'app-create-request',
  imports: [
     CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDatepickerModule
  ],
  templateUrl: './create-request.html',
  styleUrl: './create-request.scss',
})
export class CreateRequest implements OnInit {

    private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private requestService = inject(SponsorshipRequestService);
  private typeService = inject(SponsorshipTypeService);
  private router = inject(Router);
  sponsorshipTypes: any[] = [];
  
  form = this.fb.group({

    title: ['', Validators.required],

    department: ['', Validators.required],

    sponsorshipTypeId: ['', Validators.required],

    eventName: ['', Validators.required],

    eventDate: ['', Validators.required],

    requestedAmount: [0, Validators.required],

    purpose: ['', Validators.required],

    expectedBusinessBenefit: [''],

    remarks: ['']
  });

  ngOnInit(): void {

    this.loadTypes();
  }

  loadTypes(): void {

    this.typeService
      .getAll()
      .subscribe(response => {

        this.sponsorshipTypes =
          (response as any).data ??
          response;
      });
  }

  saveDraft(): void {

    this.requestService
      .createDraft(this.form.value)
      .subscribe(() => {

        this.router.navigate(['/requests']);
      });
  }

  
}
