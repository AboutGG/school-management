import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListResponse } from '../models/listResponse';
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
  
}
