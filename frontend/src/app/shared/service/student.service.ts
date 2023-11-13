import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Students } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) { }


  getStudentsReports(id: string, params: HttpParams): Observable<Blob>{
    const headers = new HttpHeaders();
    return this.http.get<Blob>(`https://localhost:7262/api/students/${id}/reports`, {params, headers, responseType: 'blob' as 'json' })

  }
  getStudentsSchoolYears(id: string): Observable<string[]>{
    return this.http.get<string[]>(`https://localhost:7262/api/students/${id}/school_years`)

  }
  getStudents(): Observable<Students>{
    return this.http.get<Students>(`https://localhost:7262/api/students`)
  }
}
