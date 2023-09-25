import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClassDetails, Classroom, ListResponse, Students, TeacherSubject, Teachers } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {

  constructor(private http: HttpClient) { }


  // get teachers classes
  getDataClassroom(params: HttpParams): Observable<Classroom[]>{
    return this.http.get<Classroom[]>(`https://localhost:7262/api/teachers/classrooms`,{params})

  }

  //get single class id
  getSingleClassroom(id: string): Observable<ClassDetails>{
    return this.http.get<ClassDetails>(`https://localhost:7262/api/classrooms/${id}`)
    
  }

  //get teacher subjects 
  getTeacherSubjects(params: {}): Observable<ListResponse> {
    return this.http.get<ListResponse>(`https://localhost:7262/api/teachers/subjects`, {params})
  }
  

  
}
