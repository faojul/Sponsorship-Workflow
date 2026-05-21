import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule }
from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { SponsorshipRequestService }
from '../../services/sponsorship-request';

import { SponsorshipTypeService }
from '../../../sponsorship-types/services/sponsorship-type';
import { AuthService } from '../../../../core/auth/auth';
import { MatSnackBar } from '@angular/material/snack-bar';
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
    MatDatepickerModule,
    MatNativeDateModule,
  ],
  providers: [DatePipe],
  templateUrl: './create-request.html',
  styleUrl: './create-request.scss',
})
export class CreateRequest implements OnInit {

    private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private requestService = inject(SponsorshipRequestService);
  private typeService = inject(SponsorshipTypeService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);
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
      .subscribe( {
next: () => {
        // Success case
        this.snackBar.open('Request submitted successfully.', 'Close', { duration: 3000 });
        this.router.navigate(['/requests']);
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
