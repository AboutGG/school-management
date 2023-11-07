import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListResponse } from '../models/listresponse';
import { Observable } from 'rxjs';
import { PdfCirculars } from '../models/pdf';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private http: HttpClient) { }

  getCount() {
    return this.http.get<any>(`https://localhost:7262/api/details/count`)
  }

  addCirculars(form: FormGroup): Observable<PdfCirculars> {
    return this.http.post<PdfCirculars>(`https://localhost:7262/api/pdf/circulars`, form);
  }

  getCirculars(params?: HttpParams): Observable<ListResponse<any>>{
    const headers = new HttpHeaders({
      'Cache-Control': 'no-cache, no-store, must-revalidate',
      'Pragma': 'no-cache',
      'Expires': '0'
    });
    return this.http.get<ListResponse<any>>(`https://localhost:7262/api/pdf/circulars`, {params, headers})

  }

  getCircularsById(id: string): Observable<Blob>{
    const headers = new HttpHeaders();
    return this.http.get<Blob>(`https://localhost:7262/api/pdf/circulars/${id}`,{ headers, responseType: 'blob' as 'json' } )

  }
}

