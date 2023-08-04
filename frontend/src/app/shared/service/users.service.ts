import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { User, Users } from '../models/users';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  addTeacher = (user: FormGroup): Observable<Users> => {
    return this.http.post<Users>(
      `https://localhost:7262/api/users/teacher`, user
    );
  };

}
