import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { StudentExams, TeacherExam } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  constructor(private http: HttpClient) { }

  getStudentExams() {
    return this.http.get<StudentExams>(`https://localhost:7262/api/students/exams`)
  }

  getTeacherExams(params: {}) {
    return this.http.get<TeacherExam[]>(`https://localhost:7262/api/teachers/exams`, { params })
  }
  
}
