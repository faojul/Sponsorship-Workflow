import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { AuthService } from '../auth/auth';

export const authGuard: CanActivateFn = () => {

 const authService = inject(AuthService);
  const router = inject(Router);
  const token = authService.getToken();

  if (token) {
    try {
      // 1. Extract the payload section of the JWT string
      const payloadBase64 = token.split('.')[1];
      const decodedPayload = JSON.parse(atob(payloadBase64));

      // 2. JWT expiration claim 'exp' is in seconds, JavaScript uses milliseconds
      const expirationDate = decodedPayload.exp * 1000;
      const isExpired = Date.now() >= expirationDate;

      if (!isExpired) {
        return true; // Token exists AND is still valid!
      }
      
      // Token is expired, clean it up before redirecting
      authService.logout(); // Or localStorage.removeItem('token')
    } catch (e) {
      // If the token is corrupted or unparsable, force logout
      authService.logout();
    }
  }

  router.navigate(['/login']);

  return false;
};