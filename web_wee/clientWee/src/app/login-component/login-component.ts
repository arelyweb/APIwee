import { Component , OnInit } from '@angular/core';
import { LoginService } from '../login.service';
import { CommonModule } from '@angular/common'; // Required for ngFor etc. in standalone
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { User } from '../../model/user.model';

@Component({
  selector: 'app-login-component',
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule],
   standalone: true,
})
export class LoginComponent implements OnInit {
    users: User[] = [];

     constructor(public _loginService: LoginService) {}
     form = new FormGroup({
    nameUser: new FormControl('', [Validators.required]),
    passUser: new FormControl('', Validators.required)
  });

  

  submit() {
    if (this.form.valid) {
              this._loginService.getUsers().subscribe({
          next: (data) => {
            console.log(data)
            this.users = data;
          },
          error: (err) => {
            console.error('Error fetching users:', err);
          }
        });
    }
  }
    
    ngOnInit(): void {

      }

}
