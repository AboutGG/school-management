import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  
  getStudentExams() {
    return this.http.get<any>(`https://localhost:7262/api/students/exams`)
  }
//TODO: cambiare tutte le response di visualizzazione liste con ListResponse,
//dopo accorpare alla chiave data il tipo specifico precedentemente utilizzato per la chiamata
  
}
