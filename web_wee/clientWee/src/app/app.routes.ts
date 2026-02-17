import { Routes } from '@angular/router';
import { LoginComponent } from './login-component/login-component';
import { RegisterComponent } from './register-component/register-component';

export const routing: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full'},
  { path: 'login', component: LoginComponent,},
  { path: 'register', component: RegisterComponent,},
  { path: '**', redirectTo: 'login' } // opcional: fallback
];