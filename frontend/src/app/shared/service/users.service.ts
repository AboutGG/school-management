import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { Users } from '../models/users';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  addTeacher = (user: FormGroup): Observable<Users> => {
    var dummy: Users = {
      user: {
        username: user.get('username')!.value,
        password: user.get('password')!.value
      },
      registry: {
        name: user.get('name')!.value,
        surname: user.get('surname')!.value,
        gender: user.get('gender')!.value,
        birth: user.get('birth')!.value,
        email: user.get('email')!.value,
        address: user.get('address')!.value,
        telephone: user.get('telephone')!.value,
      },
    }
    console.log(dummy);
    return this.http.post<Users>(
      `https://localhost:7262/api/users/teacher`, dummy
    );
  };

  addStudent = (user: FormGroup): Observable<Users> => {
    var dummy: Users = {
      classroom: user.get('classroom')!.value,
      user: {
        username: user.get('username')!.value,
        password: user.get('password')!.value,
        
      },
      registry: {
        name: user.get('name')!.value,
        surname: user.get('surname')!.value,
        gender: user.get('gender')!.value,
        birth: user.get('birth')!.value,
        email: user.get('email')!.value,
        address: user.get('address')!.value,
        telephone: user.get('telephone')!.value,

      },
    }
    console.log(dummy);
    return this.http.post<Users>(
      `https://localhost:7262/api/users/student`, dummy
    );
  };
}
