import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { StudentExams, TeacherExam } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  // getExams(order: string, orderType: string, key: string) {
  //   return this.http.get<any>(`https://localhost:7262/api/students/exams?Order=${order}&orderType=${orderType}&id=${key}`)
  // }
  getStudentExams() {
    return this.http.get<StudentExams>(`https://localhost:7262/api/students/exams`)
  }

  getTeacherExams(page: number, filter: string, search: string, orderType: string, order: string, itemsPerPage: number) {
    return this.http.get<TeacherExam[]>(`https://localhost:7262/api/teachers/exams?Page=${page}&Filter=${filter}&Search=${search}&OrderType=${orderType}&Order=${order}&ItemsPerPage=${itemsPerPage}`)
  }
  
}
