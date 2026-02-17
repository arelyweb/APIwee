import { Injectable } from '@angular/core';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../model/user.model';


@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = '/api/auth/'; // The backend API endpoint
  private baseUrl = 'http://localhost:26176';
  constructor(private http: HttpClient) { }

   // Método para cambiar la URL base del servicio
  changeServer(newUrl: string) {
    this.baseUrl = newUrl;
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  // Example of a POST request
  createUser(userData: User): Observable<{ created_IdUser: number }> {
    return this.http.post<{ created_IdUser: number }>( this.baseUrl+this.apiUrl+'register', userData);
  }
}
