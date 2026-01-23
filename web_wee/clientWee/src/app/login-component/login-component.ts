import { Component } from '@angular/core';
import { UserService } from '../user.service/user.service';

@Component({
  selector: 'app-login-component',
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
})
export class LoginComponent {
    email = '';
    password = '';
    emailInvalid = false; // Add logic to set this on validation

     constructor(public userService: UserService) {}

     login() {
    const user = { email: this.email, password: this.password };
    this.userService.login(user).subscribe((data) => {
      console.log(data);
    });
  }

}
