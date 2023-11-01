import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, take } from 'rxjs';
import { Tarefa } from '../model/Tarefa';
import { Constants as AppConstants } from '../util/constants';
import { Usuario } from '../model/Usuario';


@Injectable()
export class TarefaService {
  baseUrl = AppConstants.API_TAREFA;
  constructor(private http: HttpClient) { }

  public incluirTarefas(tarefa: Tarefa): Observable<Tarefa> {
    return this.http
    .post<Tarefa>(`${this.baseUrl}incluir/`, tarefa)
    .pipe(take(1));
  }

  public alterarTarefas(tarefa: Tarefa): Observable<Tarefa> {
    return this.http
    .put<Tarefa>(`${this.baseUrl}alterar/`, tarefa)
    .pipe(take(1));
  }

  public listarTarefas(): Observable<Tarefa[]> {
    return this.http
    .get<Tarefa[]>(`${this.baseUrl}listar`)
    .pipe(take(1));

  }

  public listarTarefasId(id: number): Observable<Tarefa> {
    return this.http
    .post<Tarefa>(`${this.baseUrl}listarid/`,{'id': id})
    .pipe(take(1));
  }

  public deletarTarefa(id: number): Observable<Tarefa> {
    return this.http
    .post<Tarefa>(`${this.baseUrl}deletar/`,{'id': id})
    .pipe(take(1));
  }
}
