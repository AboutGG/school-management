import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(private authService: AuthService, private router: Router) { }

  showInvalidInput: boolean = false;
  error: boolean = false;
  passwordInputType: string = 'password';

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
    })
  }

  login() {
    this.error = false
    this.authService.login(this.loginForm.value).subscribe((res: any) => {
      localStorage.setItem('token', res.token);
      localStorage.setItem('role', res.role);
      console.log("Sono res di login.ts", res);
      console.log("Sono token di login.ts " + res.token);
      this.router.navigate(['']);
    }, () => {
      this.error = true;
    })
  }

  showPassword() {
    this.passwordInputType === 'password' ? this.passwordInputType = 'text' : this.passwordInputType = 'password'
  }

}

