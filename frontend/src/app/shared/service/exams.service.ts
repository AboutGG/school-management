import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  constructor(private http: HttpClient) { }

  getExams(order: string, orderType: string, key: string) {
    return this.http.get<any>(`https://localhost:7262/api/student/exams?Order=${order}&orderType=${orderType}&id=${key}`)
  }

  // deleteExam(id: string) {
  //   return this.http.delete<any>(`https://localhost:7262/api/teacher/exams/:${id}`);
  // };
}
