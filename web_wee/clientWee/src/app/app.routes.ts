import { Routes } from '@angular/router';
import { LoginComponent } from './login-component/login-component';
import { RegisterComponent } from './register-component/register-component';
import { DashboardComponent } from './dashboard-component/dashboard-component';
import { LoginGuard } from './guards/login-guard';

export const routing: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full'},
  { path: 'login', component: LoginComponent,}, // solo si tienes rol Admin},
  { path: 'register', component: RegisterComponent,},
  { path: '**', redirectTo: 'login' }, // opcional: fallback
  //{ path: 'dashboard', component:DashboardComponent, canActivate: [LoginGuard]}
];