import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ListResponse } from "../models/listresponse";

@Injectable({
  providedIn: "root",
})
export class SubjectService {
  constructor(private http: HttpClient) {}

  getSubjects(params: {}): Observable<ListResponse<any>> {
    return this.http.get<ListResponse<any>>(
      "https://localhost:7262/api/students/subjects", {params}
    );
  }
}
