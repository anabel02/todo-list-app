import { HttpErrorResponse, HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, BehaviorSubject, catchError, filter, finalize, switchMap, take, throwError } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';

let isRefreshing = false;
const refreshTokenSubject = new BehaviorSubject<string | null>(null);

export function authInterceptor(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {
  const authService = inject(AuthService);
  const router = inject(Router);

  const accessToken = localStorage.getItem('access_token');
  let authReq = req;
  if (accessToken) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  }

  return next(authReq).pipe(
    catchError((error: any) => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        return handle401Error(authReq, next, authService, router);
      }
      return throwError(() => error);
    })
  );
}

function handle401Error(req: HttpRequest<any>, next: HttpHandlerFn, authService: AuthService, router: Router): Observable<HttpEvent<any>> {
  const refreshToken = localStorage.getItem('refresh_token');
  if (!refreshToken) {
    authService.logout();
    router.navigate(['/login']);
    return throwError(() => new Error('No refresh token'));
  }

  if (!isRefreshing) {
    isRefreshing = true;
    refreshTokenSubject.next(null);

    return authService.refreshToken(refreshToken).pipe(
      switchMap((res) => {
        localStorage.setItem('access_token', res.access_token);
        localStorage.setItem('refresh_token', res.refresh_token);
        refreshTokenSubject.next(res.access_token);
        return next(addTokenHeader(req, res.access_token));
      }),
      catchError(err => {
        authService.logout();
        router.navigate(['/login']);
        return throwError(() => err);
      }),
      finalize(() => {
        isRefreshing = false;
      })
    );
  } else {
    return refreshTokenSubject.pipe(
      filter(token => token !== null),
      take(1),
      switchMap(token => next(addTokenHeader(req, token!)))
    );
  }
}

function addTokenHeader(req: HttpRequest<any>, token: string) {
  return req.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
}
