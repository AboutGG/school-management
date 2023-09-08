import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  // getExams(order: string, orderType: string, key: string) {
  //   return this.http.get<any>(`https://localhost:7262/api/students/exams?Order=${order}&orderType=${orderType}&id=${key}`)
  // }
  getExams() {
    return this.http.get<any>(`https://localhost:7262/api/students/exams`)
  }

}
