import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
// import { User } from 'src/app/models/data';

interface User {
  name: string;
  surname: string;
  age: number;
  role: string
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) { }

  users: User[] = [
    {
      name: 'Giacomo',
      surname: 'Cappello',
      age: 35,
      role: 'Administrator'
    },
    {
      name: 'Sergio',
      surname: 'Musumeci',
      age: 35,
      role: 'Administrator'
    },
    {
      name: 'Mirko',
      surname: 'Amato',
      age: 24,
      role: 'Administrator'
    }
  ]

  // login(user: User) {
  //   return this.http.post(`https://dummyjson.com/auth/login`, user).subscribe((res: any) => {
  //       localStorage.setItem('token', res.token);
  //       this.router.navigate(['/home']);
  //   })
  // }

  login(user: User) {
    this.users.find(input => input.name === user.name)
    localStorage.setItem('user', JSON.stringify(user));
    this.router.navigate(['/dashboard']);
    console.log("ciaoooo");
  }

  logout() {
    // localStorage.removeItem('token')
    localStorage.removeItem('user')
    this.router.navigate(['/login'])
  }

  getToken() {
    // return localStorage.getItem('token')
    return localStorage.getItem('user')
  }

  isLogged() {
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

