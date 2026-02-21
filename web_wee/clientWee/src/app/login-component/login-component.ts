import { Component , OnInit, inject } from '@angular/core';
import { LoginService } from '../services/login.service';
import { CommonModule } from '@angular/common'; // Required for ngFor etc. in standalone
import { ReactiveFormsModule, FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition,} from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Router, withViewTransitions, isActive } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LoginUser } from '../../model/loginUser.model';

@Component({
  selector: 'app-login-component',
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatProgressSpinnerModule,],
   standalone: true,
})
export class LoginComponent implements OnInit {
    user = new LoginUser();
    isLoading = false;
    errorMessage = '';
     myGroupLogin = new FormGroup({
      userName: new FormControl(''),
      password: new FormControl(''),
    });
      private _msg = inject(MatSnackBar);
      horizontalPosition: MatSnackBarHorizontalPosition = 'center';
      verticalPosition: MatSnackBarVerticalPosition = 'top';
     
      constructor(public _loginService: LoginService,private _fb: FormBuilder, private routing: Router) {}
  
  ngOnInit(): void {
    this.myGroupLogin = this._fb.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(3)]]  
    });
  }
  

   submit(): void {
    if (this.myGroupLogin.invalid) return;

    this.isLoading = true;
    this.errorMessage = '';

    const credentials: LoginUser = {
      nameUser: this.myGroupLogin.get('userName')?.value || '',
      password: this.myGroupLogin.get('password')?.value || ''
    };

    this._loginService.login(credentials).subscribe({
      next: response => {
        console.log('Login exitoso', response);
        this.routing.navigate(['/dashboard']); // redirige a tu ruta principal
      },
      error: err => {
        this.isLoading = false;
        this.errorMessage = err?.error?.message || 'Error al iniciar sesión. Intenta de nuevo.';
        console.error('Login error:', err);
      },
      complete: () => (this.isLoading = false)
    });
  }

 openMsg(msj: string) {
    this._msg.open(msj, 'Cerrar', {
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
    });
  }
}
