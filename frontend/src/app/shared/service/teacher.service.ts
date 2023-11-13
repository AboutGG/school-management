import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponse } from '../models/listresponse';
import { IdName } from '../models/exams';
@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  constructor(private http: HttpClient) { }

  //get teacher subjects 
  getTeacherSubjects(userId: string): Observable<IdName[]> {
    return this.http.get<IdName[]>(`https://localhost:7262/api/teachers/${userId}/subjects`)
  }

  // get all classes
  getDataClassroom(): Observable<ListResponse<any>> {
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/teachers/classrooms`)
  }

  getTeacherSubjectsByClassroom(userId: string, params?: HttpParams) : Observable<IdName[]> {
    return this.http.get<IdName[]>(`https://localhost:7262/api/teachers/${userId}/subjects`, { params })
  }

}