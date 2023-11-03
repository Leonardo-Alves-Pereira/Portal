import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoginService } from '../service/login.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  constructor(private loginService: LoginService, private router: Router, private toastr: ToastrService) { }
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        for (const erro of error.error) {
          if (error.status === 0 || error.status >= 399) {
            this.toastr.error(erro.mensagem, 'Erro!', {
              timeOut: 10000
            });
          }
        }
        return throwError(error);
      })
    );
  }
}
