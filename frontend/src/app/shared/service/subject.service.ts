import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponse, StudentSubjects, Teachers } from '../models/users';


@Injectable({
  providedIn: 'root',
})
export class SubjectService {
 

  constructor(private http: HttpClient) {}

  getSubjects(params: {}): Observable<ListResponse> {
    return this.http.get<ListResponse>('https://localhost:7262/api/students/subjects', {params} )
    
  }


}
 