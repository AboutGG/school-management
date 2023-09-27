import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListResponse } from '../models/listResponse';
import { StudentExams } from '../models/users';
@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  constructor(private http: HttpClient) { }

  getStudentExams() {
    return this.http.get<StudentExams>(`https://localhost:7262/api/students/exams`)
  }
//TODO: cambiare tutte le response di visualizzazione liste con ListResponse,
//dopo accorpare alla chiave data il tipo specifico precedentemente utilizzato per la chiamata
  getTeacherExams(params: HttpParams) {
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/teachers/exams`, { params })
  }
  
}
