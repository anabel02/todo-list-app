import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environment';
import { Observable, tap } from 'rxjs';

export interface AuthResponse {
  access_token: string;
  refresh_token: string;
  token_type: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = `${environment.authApiBaseUrl}/auth`;

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<AuthResponse> {
    const body = new FormData();
    body.append('username', username);
    body.append('password', password);
    return this.http.post<AuthResponse>(`${this.apiUrl}/token`, body).pipe(
      tap(res => this.storeTokens(res))
    );
  }

  register(username: string, password: string): Observable<AuthResponse> {
    const body = new FormData();
    body.append('username', username);
    body.append('password', password);
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, body).pipe(
      tap(res => this.storeTokens(res))
    );
  }

  refreshToken(refreshToken: string): Observable<AuthResponse> {
    const body = new FormData();
    body.append('refreshToken', refreshToken);
    return this.http.post<AuthResponse>(`${this.apiUrl}/refresh`, body).pipe(
      tap(res => this.storeTokens(res))
    );
  }

  getAccessToken(): string | null {
    return localStorage.getItem('access_token');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refresh_token');
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
  }

  private storeTokens(res: AuthResponse) {
    if (res?.access_token) {
      localStorage.setItem('access_token', res.access_token);
    }
    if (res?.refresh_token) {
      localStorage.setItem('refresh_token', res.refresh_token);
    }
  }
}
