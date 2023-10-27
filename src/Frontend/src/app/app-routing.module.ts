import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


import { ProdutosComponent } from './components/produtos/produtos.component';
import { ProdutoDetalheComponent } from './components/produtos/produto-detalhe/produto-detalhe.component';
import { ProdutoListaComponent } from './components/produtos/produto-lista/produto-lista.component';

import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '',
    children: [
      { path: 'Produtos', redirectTo: 'Produtos/lista' },
      {
        path: 'Produtos',
        component: ProdutosComponent,
        children: [
          { path: 'detalhe/:id', component: ProdutoDetalheComponent },
          { path: 'detalhe', component: ProdutoDetalheComponent },
          { path: 'lista', component: ProdutoListaComponent },
        ],
      },
    ],
  },
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
