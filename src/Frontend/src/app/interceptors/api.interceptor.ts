import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LoginService } from '../service/login.service';
import { SpinnerService } from '../service/spinner.service';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  constructor(
    private toastr: ToastrService,
    private router: Router,
    private loginService: LoginService,
    private spinnerService: SpinnerService
  ) { }

  private handleSuccess(response: HttpResponse<any>): void {
    const method = response.url?.split('/').filter(Boolean).pop();
    this.toastr.success(`Sucesso na requisição para ${method}!`, 'Sucesso!', { timeOut: 10000 });
    this.router.navigateByUrl('/tarefas');
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Erro desconhecido';

    if (error.status === 0) {
      errorMessage = 'Erro de conexão';
    } else if (error.status === 401 && !error.url?.includes('user/login')) {
      errorMessage = error.error?.mensagem || 'Acesso não autorizado';
      this.loginService.logout();
    } else if (error.error) {
      errorMessage = error.error[0]?.mensagem || error.error.mensagem || errorMessage;
    }

    this.toastr.error(errorMessage, 'Erro!', { timeOut: 10000 });
    return throwError(() => errorMessage);
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.spinnerService.showSpinner();
    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse && (event.status === 201 || event.status === 204))
        this.handleSuccess(event);
      }),
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }
}
