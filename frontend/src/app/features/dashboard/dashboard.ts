import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router'; // 1. Add this import
import { AuthService } from '../../core/auth/auth';

@Component({
  selector: 'app-dashboard',
  imports: [MatCardModule, 
    RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {
constructor(
  public authService: AuthService
) {

}
}
