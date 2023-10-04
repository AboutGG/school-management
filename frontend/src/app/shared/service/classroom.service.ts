import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClassDetails } from '../models/classrooms';
import { ListResponse } from '../models/listresponse';
import { Classroom } from '../models/classrooms';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {

  constructor(private http: HttpClient) { }


  // get teachers classes
  getDataClassroom(params?: {}): Observable<ListResponse<any>>{
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/teachers/classrooms`,{params})

  }

  //get single class id
  getSingleClassroom(id: string, params: {}): Observable<ListResponse<any>>{
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/classrooms/${id}`, {params})
    
  }

  //get teacher subjects 
  getTeacherSubjects(params: {}): Observable<ListResponse<any>> {
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/teachers/subjects`, {params})
  }
  

  
}
