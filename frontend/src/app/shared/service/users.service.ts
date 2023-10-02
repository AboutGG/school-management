import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { Classroom, ListResponse, Registry, Users } from '../models/users';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: "root",
})
export class UsersService {

  role!: string
  newUser!: FormGroup

  constructor(private http: HttpClient) {}

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

  getUsers(order?: string, orderType?: string, page?: number, filter?: string, search?: string): Observable<ListResponse> {
    return this.http.get<ListResponse>(
      `https://localhost:7262/api/users?Order=${order}&OrderType=${orderType}&Page=${page}&Filter=${filter}&Search=${search}&ItemsPerPage=10`
    );
  }

  deleteUser = (id: string): Observable<Registry> => {
    return this.http.delete<Registry>(`https://localhost:7262/api/users/${id}`);
  };

  getDetailsUser(id: string): Observable<Registry> {
    return this.http.get<Registry>(`https://localhost:7262/api/details/${id}`);
  }

   getClassroom(): Observable<Classroom[]> {
     return this.http.get<Classroom[]>(`https://localhost:7262/api/classrooms`);
   }
}
