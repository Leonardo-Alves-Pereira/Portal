import { Produto } from './../models/Produto';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { PaginatedResult } from '@app/models/Pagination';

@Injectable(
// { providedIn: 'root'}
)
export class Produtoservice {
  baseURL = environment.apiURL + 'api/produtos';

  constructor(private http: HttpClient) { }

  public getProdutos(page?: number, itemsPerPage?: number, term?: string): Observable<PaginatedResult<Produto[]>> {
    const paginatedResult: PaginatedResult<Produto[]> = new PaginatedResult<Produto[]>();

    let params = new HttpParams;

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (term != null && term != '')
      params = params.append('term', term)

    return this.http
      .get<Produto[]>(this.baseURL, {observe: 'response', params })
      .pipe(
        take(1),
        map((response) => {
          paginatedResult.result = response.body;
          if(response.headers.has('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        }));
  }

  public getProdutoById(id: number): Observable<Produto> {
    return this.http
      .get<Produto>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public post(produto: Produto): Observable<Produto> {
    return this.http
      .post<Produto>(this.baseURL, produto)
      .pipe(take(1));
  }

  public put(produto: Produto): Observable<Produto> {
    return this.http
      .put<Produto>(`${this.baseURL}/${produto.id}`, produto)
      .pipe(take(1));
  }

  public deleteProduto(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  postUpload(produtoId: number, file: File): Observable<Produto> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Produto>(`${this.baseURL}/upload-image/${produtoId}`, formData)
      .pipe(take(1));
  }
}
