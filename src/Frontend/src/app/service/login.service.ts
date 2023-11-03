import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, map, take } from 'rxjs';
import { Usuario } from '../model/Usuario';
import { Constants as AppConstants } from '../util/constants';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private currentUserSource = new ReplaySubject<Usuario>(1);
currentUser$ = this.currentUserSource.asObservable();

  baseUrl = AppConstants.API_LOGIN;
  constructor(private http: HttpClient) { }

  public login(model: any): Observable<void> {
    const URL = `${this.baseUrl}/login`;
    return this.http.post<Usuario>(`${this.baseUrl}`, model).pipe(
      take(1),
      map((response: Usuario) => {
        const usuario = response;
        if (usuario)
          this.setUsuario(usuario);
      })
    );
  }

  isLogado(): boolean {
    const usuario = localStorage.getItem('usuario');
    if (usuario === null || usuario === undefined) {
      return false;
    }
    return true;
  }

  logout() {  
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    this.currentUserSource.next(null!);
    this.currentUserSource.complete();
  }

  public setUsuario(usuario: Usuario | null): void {
    if (usuario) 
      localStorage.setItem('usuario', JSON.stringify(usuario));

    this.currentUserSource.next(usuario!);
    this.currentUserSource.complete();
  }

  currentUser(): Usuario | null {
    const usuario = localStorage.getItem('usuario');
    if (usuario) {
      return JSON.parse(usuario);
    }
    return null;
  }
}
