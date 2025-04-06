import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { of } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { SessionService } from '../services/session/session.service';

export const authGuard: CanActivateFn = () => {
  const sessionService: SessionService = inject(SessionService);

  return sessionService.validate$.pipe(
    map(response => response.valid ?? false),
    catchError(() => of(false)),
    tap(valid => sessionService.loggedIn$.next(valid ?? false)),
  );
};
