import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClassDetails, Classroom, Students, Teachers } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {

  constructor(private http: HttpClient) { }

  getDataClassroom(): Observable<Classroom[]>{
    return this.http.get<Classroom[]>('https://localhost:7262/api/classroom')

  }

  getSingleClassroom(id: string): Observable<ClassDetails>{
    return this.http.get<ClassDetails>(`https://localhost:7262/api/classroom/${id}`)
    
  }
}
