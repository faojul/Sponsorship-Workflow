import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { AuthService } from '../../../core/auth/auth';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'; 


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule, 
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatProgressSpinnerModule 
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);
  private cdr = inject(ChangeDetectorRef);
  isLoading = false;

  form = this.fb.group({
    email: ['', [Validators.required]],
    password: ['', [Validators.required]]
  });

  onSubmit(): void {

    if (this.form.invalid) {
      return;
    }

    this.isLoading = true;

    this.authService.login({
      email: this.form.value.email!,
      password: this.form.value.password!
    }).subscribe({
      next: () => {
        this.isLoading = false;
        this.cdr.markForCheck();
        this.router.navigate(['/dashboard']);
      },
      error: error => {

        console.error(error);
        this.snackBar.open('Invalid email or password', 'Close', { duration: 3000 });

        this.isLoading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
