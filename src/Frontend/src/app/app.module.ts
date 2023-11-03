import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TarefasComponent } from './components/tarefas/tarefas.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './shared/nav/nav.component';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TarefaService } from './service/tarefa.service';
import { FormatoDataPipe } from './helpers/FormatoData.pipe';
import { NgxSpinnerModule } from "ngx-spinner";
import { TituloComponent } from './shared/titulo/titulo.component';
import { TarefaDetalheComponent } from './components/tarefas/tarefa-detalhe/tarefa-detalhe.component';
import { TarefaListaComponent } from './components/tarefas/tarefa-lista/tarefa-lista.component';
import { UserComponent } from './components/user/user.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { FooterComponent } from './shared/footer/footer.component';
import { LoginComponent } from './components/user/login/login.component';
import { LoginService } from './service/login.service';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { LoginInterceptor } from './interceptors/login.interceptor';
import { ApiInterceptor } from './interceptors/api.interceptor';

defineLocale('pt-br', ptBrLocale);

@NgModule({
  declarations: [
    AppComponent,
    TarefasComponent,
    TarefaDetalheComponent,
    TarefaListaComponent,
    NavComponent,
    FooterComponent,
    TituloComponent,
    FormatoDataPipe,
    UserComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
    }),
    NgxSpinnerModule,
    FormsModule
  ],
  providers: [
    TarefaService,
    LoginService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor , multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor , multi: true}

  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
