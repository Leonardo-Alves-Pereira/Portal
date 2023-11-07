import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { Observable, take, tap } from 'rxjs';
import { Usuario } from '../model/Usuario';
import { LoginService } from '../service/login.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private loginService: LoginService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser!: Usuario;
    let currentLanguage!: any;

    if (currentUser === undefined) {
      currentUser = JSON.parse(localStorage.getItem('usuario') || '{}');
      currentLanguage = JSON.parse(localStorage.getItem('language') || '{}');


      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`,
          'Accept-Language': currentLanguage
        }
      });
    }
    return next.handle(request);
  }
}



