import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClassDetails, Classroom, Students, Teachers } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {

  constructor(private http: HttpClient) { }

 // const headers = new HttpHeaders({
 //   'Authorization': `Token ${}`
 // });

  getDataClassroom(): Observable<Classroom[]>{
    return this.http.get<Classroom[]>(`https://localhost:7262/api/classrooms`,)

  }

  getSingleClassroom(id: string): Observable<ClassDetails>{
    return this.http.get<ClassDetails>(`https://localhost:7262/api/classrooms/${id}`)
    
  }

  getTeacherSubjects(): Observable<Teachers[]>{
    return this.http.get<Teachers[]>(`https://localhost:7262/api/teachers/subjects`)
  }

  
}
