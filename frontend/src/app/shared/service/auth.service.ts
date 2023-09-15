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
  constructor(private http: HttpClient) { }
  
  login(user: User) {
    return this.http.post(`${URL}/auth/login`, user);
  }

  logout() {
    localStorage.removeItem('token')
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
  
}
