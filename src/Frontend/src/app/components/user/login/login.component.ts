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

  model = {
    acceptlanguage: JSON.parse(localStorage.getItem('language') || '"pt-br"'),
  } as UserLogin;

  constructor(private http: HttpClient, private loginService: LoginService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService) { }

    ngOnInit() { 
      console.log(JSON.parse(localStorage.getItem('language') || '"pt-br"'));
    }

    login() {
      this.loginService.login(this.model).subscribe({
        next: () => {
          this.router.navigateByUrl('/user');
        },
      });
    }
}
