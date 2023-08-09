import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.example'
// import { User } from 'src/app/models/data';

const URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) { }

  response!: any
  
  // LOGIN PER DATI DAL DATABASE
  login(user: any) {
    return this.http.post(`${URL}/auth/login`, user).subscribe((res: any) => {
      this.response = res.status
      localStorage.setItem('token', res.token);
      this.router.navigate(['']);
      console.log(res);
      
    })
  }
  // LOGIN PER DATI MOCKATI
  // login(formUser: User) {
  //   this.users.find((user) => {
  //     if (user.username === formUser.username) {
  //       this.router.navigate(['/dashboard']);
  //       console.log(user);
  //     } else {
  //       this.response = false        
  //     }
  //   })
  // }

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

  handleError(error: HttpErrorResponse) {
    let msg = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      msg = error.error.message;
    } else {
      // server-side error
      msg = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(msg);
  }
}

function throwError(msg: string) {
  throw new Error('Function not implemented.');
}
