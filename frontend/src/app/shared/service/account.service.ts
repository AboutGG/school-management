import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment.example";

const URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private http: HttpClient) { }
  putUser(user: {}, id: string) {
    return this.http.put(`${URL}/users/${id}`, user);
  }
}
