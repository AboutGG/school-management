import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { Users, Registry, UsersMe } from '../models/users';
import { ListResponse } from '../models/listresponse';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: "root",
})
export class UsersService {

  role!: string
  newUser!: FormGroup;
  user! : UsersMe
  userMe: BehaviorSubject<UsersMe>

  
  constructor(private http: HttpClient) {
    this.userMe = new BehaviorSubject<UsersMe>(this.user)
   }

  getUserMeValue(): UsersMe {
    return this.userMe.value;
  }

  // addTeacher = (user: FormGroup, role: string): Observable<Users> => {
  //   let newUser: Users = {
  //     user: {
  //       username: user.get("username")!.value,
  //       password: user.get("password")!.value,
  //     },
  //     registry: {
  //       name: user.get("name")!.value,
  //       surname: user.get("surname")!.value,
  //       gender: user.get("gender")!.value,
  //       birth: user.get("birth")!.value,
  //       email: user.get("email")!.value,
  //       address: user.get("address")!.value,
  //       telephone: user.get("telephone")!.value,
  //     },
  //   };
  //   return this.http.post<Users>(
  //     `https://localhost:7262/api/users/teacher`,
  //     newUser
  //   );
  // };

  addUser = (user: FormGroup): Observable<Users> => {
    
    // let newUser: Users = {
    //   classroom: user.get("classroom")!.value,
    //   user: {
    //     username: user.get("username")!.value,
    //     password: user.get("password")!.value,
    //   },
    //   registry: {
    //     name: user.get("name")!.value,
    //     surname: user.get("surname")!.value,
    //     gender: user.get("gender")!.value,
    //     birth: user.get("birth")!.value,
    //     email: user.get("email")!.value,
    //     address: user.get("address")!.value,
    //     telephone: user.get("telephone")!.value,
    //   },
    // };

    return this.http.post<Users>(`https://localhost:7262/api/users`, user );
  };

  getUsers(order?: string, orderType?: string, page?: number, filter?: string, search?: string): Observable<ListResponse<Registry[]>> {
    return this.http.get<ListResponse<Registry[]>>(
      `https://localhost:7262/api/users?Order=${order}&OrderType=${orderType}&Page=${page}&Filter=${filter}&Search=${search}&ItemsPerPage=10`
    );
  }

  deleteUser = (id: string): Observable<Registry> => {
    return this.http.delete<Registry>(`https://localhost:7262/api/users/${id}`);
  };

  // getUsers(): Observable<any> {
  //   return this.http.get<any>('https://localhost:7262/api/users')
  // }

  getUsersMe(): Observable<UsersMe> {
    return this.http.get<UsersMe>('https://localhost:7262/api/users/me')
  }

  
  getDetailsUser(id: string): Observable<Registry> {
    return this.http.get<Registry>(`https://localhost:7262/api/details/${id}`);
  }

  editUser(form: FormGroup, id?: string): Observable<Registry> {
    return this.http.put<Registry>(`https://localhost:7262/api/details/${id}`, form);
  }

}
