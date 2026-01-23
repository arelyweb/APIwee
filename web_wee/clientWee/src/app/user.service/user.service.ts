import { Component } from '@angular/core';
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";

@Component({
  selector: 'app-user.service',
  imports: [],
  templateUrl: './user.service.html',
  styleUrl: './user.service.css',
})

@Injectable({providedIn: 'root'})
export class UserService {
  constructor(private http: HttpClient) {}
   login(user: Any): Observable<any> {
    return this.http.get("http://localhost:26176/api/users", user);
  }
}
