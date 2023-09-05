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
  response: any
  error: boolean = false;

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
    })
  }

  login() {
    if (this.loginForm.touched) {
      this.response = 'valid';
    }
    this.authService.login(this.loginForm.value).subscribe((res: any) => {
      this.response = res.status
      localStorage.setItem('token', res.token);
      localStorage.setItem('role', res.role);
      this.router.navigate(['']);
      console.log(res);
    })
    setTimeout(() => {
      this.response = 'invalid';
    }, 500);


  }
}

