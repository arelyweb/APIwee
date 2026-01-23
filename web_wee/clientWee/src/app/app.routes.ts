import { RouterModule,Routes } from '@angular/router';
import { App } from './app';
import { LoginComponent } from './login-component/login-component';

export const routing: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '/login', component: LoginComponent }
];
