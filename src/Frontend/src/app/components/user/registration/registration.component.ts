import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormatoDataPipe } from 'src/app/helpers/FormatoData.pipe';
import { Usuario } from 'src/app/model/Usuario';
import { LoginService } from 'src/app/service/login.service';
import { RegistrationService } from 'src/app/service/user/registration.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent {
  usuario!: Usuario;
  form!: FormGroup;

  get f(): any { return this.form.controls; }


  constructor(private fb: FormBuilder,
    private router: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private rota: Router,
    public registrationService: RegistrationService,
    private datePipe: FormatoDataPipe
  ) { }

  ngOnInit() {
    this.validarFormulario();
  }

  public validarFormulario(): void {
    this.form = this.fb.group({
      nome: ['', Validators.required],
      email: ['', Validators.required],
      senha: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      telefone: ['', Validators.required],
      dataCriacao: [this.datePipe.transform(new Date()), Validators.required],
    });
  }

  public cssValidator(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public registrarUsuario(): void {
    this.usuario = { ...this.form.value };
    this.registrationService.registrar(this.usuario).subscribe();
  }

}
