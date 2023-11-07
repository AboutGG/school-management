import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Classroom } from '../models/classrooms';
import { ListResponse } from '../models/listresponse';
import { FormGroup } from '@angular/forms';
import { Grade, HistoryGraduation, StudentGraduation } from '../models/studentgraduation';

@Injectable({
  providedIn: "root",
})
export class ClassroomService {
  constructor(private http: HttpClient) {}

  // get teachers classes
  getDataClassroom(params: {}): Observable<ListResponse<any>> {
    return this.http.get<ListResponse<any>>(
      `https://localhost:7262/api/teachers/classrooms`,
      { params }
    );
  }

  //get single class id
  getSingleClassroom(id: string, params?: {}): Observable<ListResponse<any>> {
    return this.http.get<ListResponse<any>>(
      `https://localhost:7262/api/classrooms/${id}`,
      { params }
    );
  }

  //get teacher subjects
  getTeacherSubjects(params: {}): Observable<ListResponse<any>> {
    return this.http.get<ListResponse<any>>(
      `https://localhost:7262/api/teachers/subjects`,
      { params }
    );
  }

  getClassroom(): Observable<Classroom[]> {
    return this.http.get<Classroom[]>(`https://localhost:7262/api/classrooms`);
  }

  addStudentGraduation(form: FormGroup, classroomId: string, userId: string) {
    return this.http.post<StudentGraduation>(`https://localhost:7262/api/classrooms/${classroomId}/students/${userId}/graduations`, form );
  }

  getGrade(classroomId: string, userId: string) {
    return this.http.get<Grade>(`https://localhost:7262/api/classrooms/${classroomId}/students/${userId}/grades`);
  }

  getHistoryPromotions(classroomId: string) {
    return this.http.get<HistoryGraduation>('https://localhost:7262/api/promotionshistory/classrooms/${classroomId}/students');
  }
  
}
