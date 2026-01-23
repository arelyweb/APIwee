import { RouterModule,Routes } from '@angular/router';
import {App} from "./app"
import { LoginComponent } from './login-component/login-component';
import { NgModule } from '@angular/core';

export const routes: Routes = [
    { path: '', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
