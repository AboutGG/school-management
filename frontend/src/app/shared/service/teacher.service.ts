import { Injectable } from '@angular/core';
import { Classroom,  } from '../models/users';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponse } from '../models/users';
@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  constructor(private http: HttpClient) { }

  //get teacher subjects 
  getTeacherSubjects(): Observable<ListResponse> {
    return this.http.get<ListResponse>(`https://localhost:7262/api/teachers/subjects`)
  }

  // get all classes
  getDataClassroom(): Observable<Classroom[]> {
    return this.http.get<Classroom[]>(`https://localhost:7262/api/teachers/classrooms`)
  }
}