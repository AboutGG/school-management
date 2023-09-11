import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { Router } from "@angular/router";

@Injectable()
export class ErrorsInterceptor implements HttpInterceptor {
  constructor(private router: Router) { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        if (err instanceof HttpErrorResponse) {
          if (this.router.url !== "/login") {
            if (err.status === 400 || err.status === 404) {
              this.router.navigate(["/not-found/" + err.status]);
            }
          }
        }
        return throwError(err);
      })
    );
  }
}
