import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TypeCount } from '../models/users';
import { Observable } from 'rxjs';
import { PdfCirculars } from '../models/pdf';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private http: HttpClient) { }

  getCount() {
    return this.http.get<TypeCount>(`https://localhost:7262/api/details/count`)
  }

  addCirculars(form: FormGroup): Observable<PdfCirculars> {
    return this.http.post<PdfCirculars>(`https://localhost:7262/api/pdf/circulars`, form);
  }
}

