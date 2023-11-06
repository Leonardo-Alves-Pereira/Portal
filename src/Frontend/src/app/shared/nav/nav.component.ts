import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from 'src/app/model/Usuario';
import { LoginService } from 'src/app/service/login.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  constructor(private renderer: Renderer2,
    private el: ElementRef,
    private router: Router,
    public loginService: LoginService) { }

  ngOnInit() {
    this.expandirMenu();
    this.esconderMenu();
    this.userMenu();
    this.esconderUserMenu();
  }
  public logout() {
    this.loginService.logout();
  }

  public userMenu() {
    // Seleciona o botão de toggling do offcanvas pelo seu ID e adiciona um ouvinte de evento de clique
    const togglerButton = document.getElementById('togglerUser');
    togglerButton?.addEventListener('click', () => {
      // Seleciona o offcanvas pelo seu ID
      const offcanvas = document.getElementById('offcanvasDarkNavbarEnd');
      this.renderer.removeClass(offcanvas, 'hiding');

      // Ativa o offcanvas usando a classe do Bootstrap
      this.renderer.addClass(offcanvas, 'show');

      // Coloca o foco dentro do offcanvas para torná-lo acessível
      offcanvas?.focus();
    });
  }

  public esconderUserMenu() {
    // Adiciona um ouvinte de evento para fechar o offcanvas quando o usuário clicar no botão de fechar
    const closeButton = document.getElementById('togglerCloseUser');
    closeButton?.addEventListener('click', () => {
      // Seleciona o offcanvas pelo seu ID
      const offcanvas = document.getElementById('offcanvasDarkNavbarEnd');

      this.renderer.addClass(offcanvas, 'hiding');

      // Fecha o offcanvas removendo a classe 'show'
      this.renderer.removeClass(offcanvas, 'show');
    });
  }

  public expandirMenu() {
    // Seleciona o botão de toggling do offcanvas pelo seu ID e adiciona um ouvinte de evento de clique
    const togglerButton = document.getElementById('togglerButton');
    togglerButton?.addEventListener('click', () => {
    });
  }

  public esconderMenu() {
    // Adiciona um ouvinte de evento para fechar o offcanvas quando o usuário clicar no botão de fechar
    const closeButton = document.querySelector('.btn-close');
    closeButton?.addEventListener('click', () => {
      // Seleciona o offcanvas pelo seu ID
      const offcanvas = document.getElementById('offcanvasDarkNavbar');

      this.renderer.addClass(offcanvas, 'hiding');

      // Fecha o offcanvas removendo a classe 'show'
      this.renderer.removeClass(offcanvas, 'show');

    });
  }

  navMenuShow(event: Event) {
    // Acesse o elemento que foi clicado usando event.target
    const elementoClicado = event.target as HTMLElement;

    if (elementoClicado.id == "togglerButton") {
      const offcanvas = document.getElementById('offcanvasDarkNavbar');
      this.renderer.removeClass(offcanvas, 'hiding');
      this.renderer.addClass(offcanvas, 'show');
      offcanvas?.focus();
    } else if (elementoClicado.id == "togglerUser") {
      const offcanvas = document.getElementById('offcanvasDarkNavbarEnd');
      this.renderer.removeClass(offcanvas, 'hiding');
      this.renderer.addClass(offcanvas, 'show');
      offcanvas?.focus();
    } else if (elementoClicado.id == "btnClose") {
      const offcanvas = document.getElementById('offcanvasDarkNavbar');
      this.renderer.addClass(offcanvas, 'hiding');
      this.renderer.removeClass(offcanvas, 'show');
    } else if (elementoClicado.id == "btnUserClose") {
      const offcanvas = document.getElementById('offcanvasDarkNavbarEnd');
      this.renderer.addClass(offcanvas, 'hiding');
      this.renderer.removeClass(offcanvas, 'show');
    }

  }

  public esconderNav() {
    return this.router.url === '/user/login' || this.router.url === '/user/registrar';
  }

}
