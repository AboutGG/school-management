import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.example';
import { User } from '../models/users';
import { Router } from '@angular/router';

const URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) { }
  
  login(user: User) {
    return this.http.post(`${URL}/auth/login`, user);
  }

  logout() {
    localStorage.removeItem('token')
    localStorage.removeItem('role')
    this.router.navigate(['/login']);
  }

  getToken() {
    return localStorage.getItem('token')
  }

  getRole() {
    return localStorage.getItem('role')
  }

  isLoggedIn() {
    return this.getToken() !== null ? true : false
  }

  isTeacher() {
    return this.getRole() == 'teacher'.toLowerCase() ? true : false
  }
  
  changePassword(user: {}, id: string) {
    return this.http.put(`${URL}/users/${id}`, user);
  }

}
