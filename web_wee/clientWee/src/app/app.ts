import { Component, OnInit,Injectable, inject, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterOutlet,RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { LoginService } from './services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [CommonModule, RouterOutlet, RouterLink, MatToolbarModule, MatButtonModule],
  standalone: true,
})

@Injectable({providedIn: 'root'})

export class App {

  private http = inject(HttpClient);
 isAuthenticated$ ;
  currentUser$ ;

    constructor(private loginService: LoginService) {
    this.isAuthenticated$ = this.loginService.isAuthenticated$;
    this.currentUser$ = this.loginService.currentUser$;
    }

      logout(): void {
    this.loginService.logout().subscribe({
      next: () => console.log('Logout exitoso'),
      error: err => console.error('Logout error:', err)
    });
  }


  title = 'clientWee';

}
