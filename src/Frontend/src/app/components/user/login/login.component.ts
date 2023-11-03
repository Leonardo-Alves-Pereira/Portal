import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { UserLogin } from 'src/app/model/userLogin';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginService } from 'src/app/service/login.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {

  model = {} as UserLogin;

  constructor(private http: HttpClient, private loginService: LoginService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService) { }

  login() {
    this.loginService.login(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('/user');
      },
      error: (error) => { 
        if (error.status == 401) {
          this.toastr.error(error.error.mensagens, 'Erro', {
            timeOut: 10000
          });
        }
         },
      complete: () => { }
    });
  }
}
