import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { LoginService } from '../service/login.service';

@Injectable()
export class LoginInterceptor implements HttpInterceptor {
  constructor(private router: Router, private loginSevice: LoginService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<any>> {
    


    return next.handle(request);

  }
}
