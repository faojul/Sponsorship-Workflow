import { inject } from '@angular/core';
import { CanActivateChildFn, CanActivateFn, Router } from '@angular/router';

import { AuthService } from '../auth/auth';

function checkAuth(): boolean {

  const authService = inject(AuthService);
  const router = inject(Router);
  const token = authService.getToken();

  if (!token) {
    router.navigate(['/login']);
    return false;
  }

  try {
    // Extract the payload section of the JWT string
    const payloadBase64 = token.split('.')[1];
    const decodedPayload =
      JSON.parse(
        atob(payloadBase64)
      );
    
    // JWT expiration claim 'exp' is in seconds, JavaScript uses milliseconds
    const expirationDate = decodedPayload.exp * 1000;

    const isExpired = Date.now() >= expirationDate;

    if (isExpired) {
      authService.logout();
      router.navigate(['/login']);
      return false;
    }

    return true;

  } catch {
    authService.logout();
    router.navigate(['/login']);
    return false;
  }
}

export const authGuard:
CanActivateFn = () => {

  return checkAuth();
};

export const authChildGuard:
CanActivateChildFn = () => {

  return checkAuth();
};