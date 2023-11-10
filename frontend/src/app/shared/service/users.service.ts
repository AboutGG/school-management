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

  addUser = (user: FormGroup): Observable<Users> => {
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
