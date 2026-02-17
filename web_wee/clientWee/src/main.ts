import { bootstrapApplication } from '@angular/platform-browser';
import { importProvidersFrom } from '@angular/core';
import { RouterModule,  withViewTransitions } from '@angular/router';
import { App } from './app/app';
import { routing } from './app/app.routes';
bootstrapApplication(App, {
  providers: [
    importProvidersFrom(
      RouterModule.forRoot(routing,{enableViewTransitions: true}), 
    ),
  ]
}).catch((err) => console.error(err));
