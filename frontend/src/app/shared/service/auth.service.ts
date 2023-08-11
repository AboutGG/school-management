import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.example';
import { User } from '../models/users';

const URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) { }

  response!: any
  
  // LOGIN PER DATI DAL DATABASE
  login(user: User) {
    return this.http.post(`${URL}/auth/login`, user).subscribe((res: any) => {
      this.response = res.status
      localStorage.setItem('token', res.token);
      this.router.navigate(['']);
      console.log(res);
      
    })
  }

  logout() {
    localStorage.removeItem('token')
    this.router.navigate(['/login'])
  }

  getToken() {
    return localStorage.getItem('token')
  }

  isLoggedIn() {
    return this.getToken() == null ? false : true
  }
  
}
