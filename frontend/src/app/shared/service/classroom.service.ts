import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClassDetails, Classroom, Students, Teacher, Teachers } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {

  constructor(private http: HttpClient) { }


  // get teachers classes
  getDataClassroom(): Observable<Classroom[]>{
    return this.http.get<Classroom[]>(`https://localhost:7262/api/teachers/classrooms`,)

  }

  //chiamata per la ricerca
  searchClassrooms(searchTerm: string): Observable<Classroom[]> {
    return this.http.get<Classroom[]>(`https://localhost:7262/api/teachers/classrooms?search=${searchTerm}`);
  }
  

  //get single class id
  getSingleClassroom(id: string): Observable<ClassDetails>{
    return this.http.get<ClassDetails>(`https://localhost:7262/api/classrooms/${id}`)
    
  }

  //get teacher subjects 
  getTeacherSubjects(): Observable<Teacher>{
    return this.http.get<Teacher>(`https://localhost:7262/api/teachers/subjects`)
  }

  
}
