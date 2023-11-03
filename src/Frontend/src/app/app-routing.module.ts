import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TarefasComponent } from './components/tarefas/tarefas.component';
import { TarefaListaComponent } from './components/tarefas/tarefa-lista/tarefa-lista.component';
import { TarefaDetalheComponent } from './components/tarefas/tarefa-detalhe/tarefa-detalhe.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { AuthGuard } from './guards/auth.guard';
import { NoAuthGuard } from './guards/noAuth.guard';

const routes: Routes = [
  { path: 'tarefas', redirectTo: 'tarefas/lista' },
  {
    path: 'tarefas', component: TarefasComponent,
    children: [
      { path: 'detalhe/:id', component: TarefaDetalheComponent, canActivate: [AuthGuard] },
      { path: 'lista', component: TarefaListaComponent, canActivate: [AuthGuard] }
    ]
  },
  { path: 'user', redirectTo: 'tarefas/lista' },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent, canActivate: [NoAuthGuard] },
      { path: 'registrar', component: RegistrationComponent,canActivate: [NoAuthGuard] }
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
