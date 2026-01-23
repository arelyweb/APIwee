import { bootstrapApplication } from '@angular/platform-browser';
import {provideZoneChangeDetection} from '@angular/core';
import { App } from './app/app';
bootstrapApplication(App, {
  providers: [provideZoneChangeDetection({eventCoalescing: true})],
}).catch((err) => console.error(err));
