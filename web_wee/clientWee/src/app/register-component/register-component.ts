import { Component, OnInit ,inject} from '@angular/core';
import { LoginService } from '../services/login.service';
import { CommonModule } from '@angular/common'; // Required for ngFor etc. in standalone
import { ReactiveFormsModule, FormGroup, FormControl, Validators, FormBuilder , FormsModule} from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition,} from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { User } from '../../model/user.model';

@Component({
  selector: 'app-register-component',
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, FormsModule],
  templateUrl: './register-component.html',
  styleUrl: './register-component.css',
  standalone: true,
})
export class RegisterComponent implements OnInit {
  users = new User();
  myGroup = new FormGroup({
    userName: new FormControl(''),
    lastName: new FormControl(''),
    password: new FormControl(''),
  });
  private _msg = inject(MatSnackBar);
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  constructor(public _loginService: LoginService,
    private _fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.myGroup = this._fb.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  submit() {
    if (this.myGroup.valid) {
      this.users.userName= this.myGroup.get('userName')?.value;
      this.users.lastName = this.myGroup.get('lastName')?.value;
      this.users.password = this.myGroup.get('password')?.value; 
      this.users.roleId= 1;

      this._loginService.createUser(this.users).subscribe({
        next: (data) => {
           this.myGroup.reset();
          this.openMsg('Usuario registrado');
        },
        error: (err) => {
          console.error('Error fetching users:', err);
           this.openMsg('No se ha registrado');
        }
      });
    }
  }

  openMsg(msj: string) {
    this._msg.open(msj, 'Cerrar', {
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
    });
  }

}
