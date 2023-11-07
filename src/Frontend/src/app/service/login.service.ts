import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, map, take } from 'rxjs';
import { Usuario } from '../model/Usuario';
import { Constants as AppConstants } from '../util/constants';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  baseUrl = AppConstants.API_LOGIN;

  constructor(private http: HttpClient,
    private router: Router) { }

  public login(model: any): Observable<void> {
    localStorage.setItem('language', JSON.stringify(model.acceptlanguage));
    const URL = `${this.baseUrl}/login`;
    return this.http.post<Usuario>(`${this.baseUrl}`, model).pipe(
      take(1),
      map((response: Usuario) => {
        const usuario = response;
        if (usuario){
          this.setUsuario(usuario);
          console.log(usuario);
        }
      })
    );
  }

  isLogado(): boolean {
    const usuario = localStorage.getItem('usuario');
    if (usuario === null || usuario === undefined) { return false; }
    return true;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    this.router.navigateByUrl('/user/login');
  }

  public setUsuario(usuario: Usuario | null): void {
    if (usuario)
      localStorage.setItem('usuario', JSON.stringify(usuario));
  }

  currentUser(): Usuario | null {
    const usuario = localStorage.getItem('usuario');
    if (usuario) { return JSON.parse(usuario); }
    return null;
  }
}
