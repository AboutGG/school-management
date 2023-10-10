import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListResponse } from '../models/listresponse';
import { StudentExams, User } from '../models/users';
import { FormGroup } from '@angular/forms';
import { TeacherExam } from '../models/teacherexam';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  constructor(private http: HttpClient) { }

  getStudentExams(params: HttpParams) {
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/students/exams`, { params })
  }

  getTeacherExams(params: HttpParams) {
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/teachers/exams`, { params })
  }

  addExam (form: any) {
    return this.http.post<TeacherExam>(`https://localhost:7262/api/exams`, form)
  }

  editExam (form: FormGroup, id?: string) {
    return this.http.put<TeacherExam>(`https://localhost:7262/api/exams/${id}`, form)
  }

  deleteExam (id: string) {
    return this.http.delete<TeacherExam>(`https://localhost:7262/api/exams/${id}`)
  }

}
