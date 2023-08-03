import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
// import { User } from 'src/app/models/data';

interface User {
  name: string;
  surname: string;
  username: string;
  password: string;
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
      username: "giaco.",
      password: 'admin'
    },
    {
      name: 'Sergio',
      surname: 'Musumeci',
      username: "sirjoh",
      password: 'Administrator'
    },
    {
      name: 'Mirko',
      surname: 'Amato',
      username: "disturbed",
      password: 'Administrator'
    }
  ]
  // LOGIN PER DATI DAL DATABASE
  // login(user: User) {
  //   return this.http.post(`https://dummyjson.com/auth/login`, user).subscribe((res: any) => {
  //       localStorage.setItem('token', res.token);
  //       this.router.navigate(['/home']);
  //   })
  // }

  // login(input: User) {
  //   this.users.map((user) => {
  //     if (user.name === input.name) {
  //       localStorage.setItem('input', JSON.stringify(input));
  //       this.router.navigate(['/dashboard']);
  //     }
  //     console.log(user);
  //   })
  // }

  login(formUser: User) {
    this.users.find((user) => {
      if (user.username === formUser.username) {
        this.router.navigate(['/dashboard']);
        console.log(user);
      }
    })
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

