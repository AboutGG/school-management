import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TypeCount } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private http: HttpClient) { }

  getCount() {
    return this.http.get<TypeCount>(`https://localhost:7262/api/details/count`)
  }

}
