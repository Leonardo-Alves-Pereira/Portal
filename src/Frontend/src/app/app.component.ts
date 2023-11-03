import { Component } from '@angular/core';
import { LoginService } from './service/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Frontend';
  constructor(private loginService: LoginService) { }

  public login(){
    let user = this.loginService.currentUser();
    this.loginService.setUsuario(user);
  }

  ngOnInit() {
    this.login();
  }
}
