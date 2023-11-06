import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Constants as AppConstants } from '../../util/constants';
import { Usuario } from 'src/app/model/Usuario';


@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  baseUrl = AppConstants.API_USUARIO;

  constructor(private http: HttpClient) { }

  public registrar(usuario: Usuario): Observable<Usuario> {
    return this.http
      .post<Usuario>(`${this.baseUrl}registro/`, usuario)
      .pipe(take(1));
  }
}
