import { bootstrapApplication } from '@angular/platform-browser';
import { importProvidersFrom } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { App } from './app/app';
import { routing } from './app/app.routes';
import { LoginInterceptor } from './app/interceptors/auth-interceptor/login-interceptor';
bootstrapApplication(App, {
  providers: [
    importProvidersFrom(
      RouterModule.forRoot(routing,{enableViewTransitions: true}), 
    ),
    { provide: HTTP_INTERCEPTORS, useClass: LoginInterceptor, multi: true }
  ]
}).catch((err) => console.error(err));
