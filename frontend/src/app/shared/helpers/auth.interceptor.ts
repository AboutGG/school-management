import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const authToken = this.authService.getToken();
    const authRole = this.authService.getRole();

    if (authToken && authRole) {
      request = request.clone({
        headers: request.headers.set("Token", authToken).set("Role", authRole),
      });
    }
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        if (err instanceof HttpErrorResponse) {
          if (this.router.url !== "/login") {
            if (err.status === 400 || err.status === 404) {
              this.router.navigate(["/not-found/" + err.status]);
            }
            else if (err.status === 401 && /\/details\/\d+/.test(this.router.url)) {
              console.log("capiamo");
              this.router.navigate(["/not-found" + err.status]);
            }
            
          }
        }
        return throwError(err);
      })
    );
  }
}
