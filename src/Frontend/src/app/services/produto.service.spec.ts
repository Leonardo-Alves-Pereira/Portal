/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { Produtoservice } from './produto.service';

describe('Service: Produto', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Produtoservice]
    });
  });

  it('should ...', inject([Produtoservice], (service: Produtoservice) => {
    expect(service).toBeTruthy();
  }));
});
