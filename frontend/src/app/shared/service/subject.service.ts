import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StudentSubjects, Teachers } from '../models/users';


@Injectable({
  providedIn: 'root',
})
export class SubjectService {
 

  constructor(private http: HttpClient) {}

  getSubjects(): Observable<StudentSubjects[]> {
    return this.http.get<StudentSubjects[]>('https://localhost:7262/api/students/subjects' )
    
  }
}
