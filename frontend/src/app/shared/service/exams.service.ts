import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { ListResponse } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  
  getStudentExams(params: HttpParams) {
    return this.http.get<ListResponse>(`https://localhost:7262/api/students/exams`, { params })
  }
//TODO: cambiare tutte le response di visualizzazione liste con ListResponse,
//dopo accorpare alla chiave data il tipo specifico precedentemente utilizzato per la chiamata
  
}
