import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-component',
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
  standalone: false,
})
export class LoginComponent {
    email = '';
    password = '';
    emailInvalid = false; // Add logic to set this on validation

    constructor(private router: Router) {}

    onSubmit() {
    // Handle login logic (e.g., call an auth service)
    if (this.email === 'test@test.com' && this.password === 'password') {
      this.router.navigate(['/dashboard']); // Use routing to navigate on success
    } else {
      this.emailInvalid = true; // Show error
    }
  }

}
