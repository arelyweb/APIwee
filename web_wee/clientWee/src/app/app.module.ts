
import { NgModule } from '@angular/core';
import {  ReactiveFormsModule } from '@angular/forms'; // Import one or both form modules
import { CommonModule } from '@angular/common'; // Import CommonModule
import { App } from './app';
import { LoginComponent } from './login-component/login-component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormsModule} from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app.routes';



// ... other imports ...

@NgModule({
  declarations: [
   App,
   LoginComponent
  ],
  imports: [
    // ... other imports
    MatCardModule,
    ReactiveFormsModule,
    CommonModule,
    MatSlideToggleModule,
     MatButtonModule,
     MatSelectModule,
     MatInputModule,
     MatFormFieldModule,
     FormsModule,
     BrowserModule,
     AppRoutingModule
    // ... other modules
  ],
  // ...
})
export class AppModule { }