import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UsersMe } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) { }


  getStudentsReports(id: string): Observable<any>{
    return this.http.get<any>(`https://localhost:7262/api/students/${id}/reports`)

  }
  getStudentsSchoolYears(id: string): Observable<string[]>{
    return this.http.get<string[]>(`https://localhost:7262/api/students/${id}/school_years`)

  }
}
