import { Component,Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap, finalize } from 'rxjs/operators';
import { LoginService } from '../../services/login.service';

@Injectable()
export class LoginInterceptor implements HttpInterceptor {
    private isRefreshing = false;

  constructor(private loginService: LoginService) {}
    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Añade el access token al header si existe
    const token = this.loginService.getAccessToken();
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(request).pipe(
      catchError(error => {
        // Si el token expiró (401), intenta refrescarlo
        if (error instanceof HttpErrorResponse && error.status === 401 && !this.isRefreshing) {
          this.isRefreshing = true;

          return this.loginService.refresh().pipe(
            switchMap((response: any) => {
              // Reintenta la petición original con el nuevo token
              const newToken = response.accessToken;
              request = request.clone({
                setHeaders: {
                  Authorization: `Bearer ${newToken}`
                }
              });
              return next.handle(request);
            }),
            catchError(refreshError => {
              // Si refresh falla, logout automático
              this.loginService.logout().subscribe();
              return throwError(() => refreshError);
            }),
            finalize(() => (this.isRefreshing = false))
          );
        }

        return throwError(() => error);
      })
    );
  }
}
