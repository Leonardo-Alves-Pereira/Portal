import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TarefasComponent } from './components/tarefas/tarefas.component';
import { TarefaListaComponent } from './components/tarefas/tarefa-lista/tarefa-lista.component';
import { TarefaDetalheComponent } from './components/tarefas/tarefa-detalhe/tarefa-detalhe.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';

const routes: Routes = [
  { path: 'tarefas', redirectTo: 'tarefas/lista' },
  {
    path: 'tarefas', component: TarefasComponent,
    children: [
      { path: 'detalhe/:id', component: TarefaDetalheComponent },
      { path: 'novo', component: TarefaDetalheComponent },
      { path: 'lista', component: TarefaListaComponent }
    ]
  },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registrar', component: RegistrationComponent }
    ]
  },
  { path: '', redirectTo: 'tarefas/lista', pathMatch: 'full' },
  { path: '**', redirectTo: 'tarefas/lista', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
