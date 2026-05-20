import { Injectable, computed, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, tap } from 'rxjs';

import { environment } from '../../../environments/environment';

import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly TOKEN_KEY = 'access_token';

  currentUser = signal<LoginResponse | null>(null);

  isAuthenticated = computed(() => !!this.currentUser());

  constructor(
    private http: HttpClient
  ) {
    this.loadUserFromStorage();
  }

  login(request: LoginRequest): Observable<LoginResponse> {

    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/Auth/login`,
      request
    ).pipe(
      tap((response: any) => {
          localStorage.setItem(this.TOKEN_KEY, JSON.stringify(response.data));
          
          this.currentUser.set({
            token: response.data.token,
            email: response.data.email,
            roles: response.data.roles
          });
        })
    );
  }

  getToken(): string | null {
      const data = localStorage.getItem(this.TOKEN_KEY);
      if (!data) return null;
      
      try {
        // Parse the JSON object and extract the token field
        return JSON.parse(data).token;
      } catch {
        return null;
      }
  }

  private loadUserFromStorage(): void {

    const data = localStorage.getItem(this.TOKEN_KEY);

    if (!data) {
      return;
    }

    try {
      const user = JSON.parse(data);
      // Hydrate your Signal fully so roles and email are available on reload
      this.currentUser.set({
        token: user.token,
        email: user.email,
        roles: user.roles
      });
    } 
    catch {
      localStorage.removeItem(this.TOKEN_KEY); // Clean up corrupted data
    }
  }

  hasRole(role: string): boolean {

    const user = this.currentUser();

    if (!user) {
      return false;
    }

    return user.roles.includes(role);
  }

  logout(): void {

  localStorage.removeItem(this.TOKEN_KEY);

  this.currentUser.set(null);

  window.location.href = '/login';
}
}