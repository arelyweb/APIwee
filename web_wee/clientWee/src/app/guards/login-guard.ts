import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable({ providedIn: 'root' })
export class LoginGuard implements CanActivate {
  constructor(private loginService: LoginService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.loginService.hasValidToken()) {
      // Token válido, permite acceso
      const user = this.loginService.getCurrentUser();

      // Opcional: verifica roles
      if (route.data['roles'] && !this.hasRole(route.data['roles'])) {
        this.router.navigate(['/unauthorized']);
        return false;
      }

      return true;
    }

    // Sin token válido, redirige a login
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }

  private hasRole(roles: string[]): boolean {
    const token = this.loginService.getAccessToken();
    if (!token) return false;

    const decoded = this.loginService.decodeToken(token);
    const userRoles = decoded?.role || []; // el claim Role puede ser array o string

    return roles.some(role =>
      Array.isArray(userRoles) ? userRoles.includes(role) : userRoles === role
    );
  }
}