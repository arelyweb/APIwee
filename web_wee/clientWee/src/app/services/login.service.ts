import { Injectable } from '@angular/core';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { User } from '../../model/user.model';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { LoginUser } from '../../model/loginUser.model';
import { TokenResponse } from '../../model/tokenResponse.model';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = '/api/auth/'; // The backend API endpoint
  private baseUrl = 'http://localhost:26176';
  private currentUserSubject = new BehaviorSubject<User | null>(this.loadUserFromStorage());
  public currentUser$ = this.currentUserSubject.asObservable();

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasValidToken());
  public isAuthenticated$ = this.isAuthenticatedSubject.asObservable();


  constructor(private http: HttpClient) { }
  
  /**
    * Método para cambiar la URL base del servicio
   */
  changeServer(newUrl: string) {
    this.baseUrl = newUrl;
  }

   /**
   * Login: envía credenciales y almacena tokens
   */
  login(credentials: LoginUser): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(this.baseUrl + this.apiUrl + 'login', credentials)
      .pipe(
        tap(response => this.handleAuthResponse(response, credentials)),
        catchError(err => {
          console.error('Login error', err);
          return throwError(() => err);
        })
      );
  }
  /**
   * Refresh: obtiene nuevo access token usando el refresh token
   */
  refresh(): Observable<TokenResponse> {
    const accessToken = this.getAccessToken();
    const refreshToken = this.getRefreshToken();

    if (!accessToken || !refreshToken) {
      return throwError(() => new Error('No tokens available'));
    }

    return this.http
      .post<TokenResponse>(this.baseUrl + this.apiUrl + 'refresh', {
        accessToken,
        refreshToken
      })
      .pipe(
        tap(response => this.handleAuthResponse(response)),
        catchError(err => {
          console.error('Refresh error', err);
          this.logout(); // si refresh falla, cierra sesión
          return throwError(() => err);
        })
      );
  }

   /**
   * Logout: revoca tokens y limpia almacenamiento
   */
  logout(): Observable<any> {
    const refreshToken = this.getRefreshToken();
    const accessToken = this.getAccessToken();

    return this.http
      .post(this.baseUrl + this.apiUrl + 'revoke', {
        accessToken,
        refreshToken
      })
      .pipe(
        tap(() => this.clearAuth()),
        catchError(err => {
          // incluso si falla revoke, limpia local
          this.clearAuth();
          return throwError(() => err);
        })
      );
  }
   /**
   * Obtiene el access token desde localStorage
   */
  getAccessToken(): string | null {
    return localStorage.getItem('access_token');
  }

  /**
   * Obtiene el refresh token desde localStorage
   */
  getRefreshToken(): string | null {
    return localStorage.getItem('refresh_token');
  }

  /**
   * Verifica si el token no ha expirado
   */
  hasValidToken(): boolean {
    const expiresAt = localStorage.getItem('expires_at');
    if (!expiresAt) return false;

    const expiryTime = new Date(expiresAt).getTime();
    return expiryTime > Date.now();
  }

  /**
   * Obtiene el usuario actual
   */
  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

   /**
   * Decodifica el JWT (sin validar firma, solo para leer claims)
   */
  decodeToken(token: string): any {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join('')
      );
      return JSON.parse(jsonPayload);
    } catch (e) {
      console.error('Error decoding token', e);
      return null;
    }
  }
/** 
 * POST para crear un nuevo usuario
*/
  createUser(userData: User): Observable<{ created_IdUser: number }> {
    return this.http.post<{ created_IdUser: number }>( this.baseUrl+this.apiUrl+'register', userData);
  }

   // -------- PRIVADAS --------

  /**
   * Maneja la respuesta de login/refresh: almacena tokens y actualiza estado
   */
  private handleAuthResponse(response: TokenResponse, credentials?: LoginUser): void {
    localStorage.setItem('access_token', response.accessToken);
    localStorage.setItem('refresh_token', response.refreshToken);
    localStorage.setItem('expires_at', new Date(response.expiresAt).toISOString());

    // Decodifica el token para extraer datos del usuario (username, roles, etc)
    const decoded = this.decodeToken(response.accessToken);
    if (decoded) {
      const user: User = {
        userName: decoded.sub || credentials?.nameUser || 'User',
        id_user: decoded.nameid // nameid es el claim NameIdentifier (id_user),
      };
      localStorage.setItem('current_user', JSON.stringify(user));
      this.currentUserSubject.next(user);
    }

    this.isAuthenticatedSubject.next(true);
  }

  /**
   * Limpia autenticación (logout)
   */
  private clearAuth(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('expires_at');
    localStorage.removeItem('current_user');
    this.currentUserSubject.next(null);
    this.isAuthenticatedSubject.next(false);
  }

  /**
   * Carga el usuario almacenado desde localStorage
   */
  private loadUserFromStorage(): User | null {
    const stored = localStorage.getItem('current_user');
    return stored ? JSON.parse(stored) : null;
  }
}
