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
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router, private jwtHelper: JwtHelperService) { }

  isTokenExpired(token: string): boolean {
    return this.jwtHelper.isTokenExpired(token);
  }

  //  redirectIfTokenExpired() {
  //    const token = this.authService.getToken();
  //    if (token && this.isTokenExpired(token)) {
  //      // Il token è scaduto, reindirizza alla pagina di login
  //      this.router.navigate(['/login']);
  //    }
  //  }
  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const authToken = this.authService.getToken();
    const authRole = this.authService.getRole();


    if (authToken && authRole && !this.isTokenExpired(authToken)) {
      request = request.clone({
        headers: request.headers.set("Token", authToken).set("Role", authRole),
      });
    } else {
      this.authService.logout()
    }



    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        if (err instanceof HttpErrorResponse) {
          if (this.router.url !== "/login") {
            if (err.status === 404) {
              this.router.navigate(["/not-found/" + err.status]);
            }
            else if (err.status === 401 && /\/details\/\d+/.test(this.router.url)) {
              console.log(err.status);
              this.router.navigate(["/not-found/" + err.status]);
            }
          }
        }
        return throwError(err);
      })
    );
  }
}

