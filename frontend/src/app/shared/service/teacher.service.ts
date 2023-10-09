import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponse } from '../models/listresponse';
@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  constructor(private http: HttpClient) { }

  //get teacher subjects 
  getTeacherSubjects(): Observable<ListResponse<any>> {
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/teachers/subjects`)
  }

  // get all classes
  getDataClassroom(): Observable<ListResponse<any>> {
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/teachers/classrooms`)
  }

  getTeacherSubjectByClassroom(userId: string, params: HttpParams) {
    return this.http.get(`https://localhost:7262/api/teachers/${userId}/subjects`, { params })
  }

}