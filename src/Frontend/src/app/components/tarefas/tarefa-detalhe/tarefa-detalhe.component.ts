import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Tarefa } from 'src/app/model/Tarefa';
import { LoginService } from 'src/app/service/login.service';
import { TarefaService } from 'src/app/service/tarefa.service';
@Component({
  selector: 'app-tarefa-detalhe',
  templateUrl: './tarefa-detalhe.component.html',
  styleUrls: ['./tarefa-detalhe.component.scss']
})
export class TarefaDetalheComponent implements OnInit {

  user = this.loginService.currentUser();
  id = this.user?.id;
  tarefa!: Tarefa;
  form!: FormGroup;

  get f(): any { return this.form.controls; }

  showSpinner() {
    this.spinner.show();
    setTimeout(() => { this.spinner.hide(); }, 5000);
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'bg-dark theme-red',
      adaptivePosition: true,
      showWeekNumbers: false,
      multipledates: true,
      locale: 'pt-br'
    };

  }

  constructor(private fb: FormBuilder,
    private router: ActivatedRoute,
    private tarefaService: TarefaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private rota: Router,
    public loginService: LoginService
  ) { }

  ngOnInit() {
    this.carregarTarefa();
    this.validarFormulario();
  }

  public validarFormulario(): void {
    this.form = this.fb.group({
      titulo: ['', Validators.required],
      dataCriacao: ['', Validators.required],
      dataConclusao: ['', Validators.required],
      usuarioId: [this.id, Validators.required],
      usuario: this.fb.group({
        nome: [''],
      }),
      descricao: ['', Validators.required],
    });
  }

  public cssValidator(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public validarTarefa(): void {
    const tarefaId = this.router.snapshot.paramMap.get('id');
    if (!tarefaId) { return }
    if (this.form.valid) {
      if (tarefaId === "0") {
        this.taskTarefa("incluirTarefas", tarefaId);
      } else {
        this.taskTarefa("alterarTarefas", tarefaId);
      }
    } else {
      this.toastr.error('Formulário inválido!', 'Erro', { timeOut: 10000 });
    }
  }

  public taskTarefa(metodoNome: string, id: string): void {
    this.tarefa = { 'id': id, ...this.form.value };
    (this.tarefaService as any)[metodoNome](this.tarefa).subscribe();
  }

  public carregarTarefa(): void {
    const tarefaId = this.router.snapshot.paramMap.get('id');

    if (tarefaId !== null && tarefaId !== "0") {
      this.tarefaService.listarTarefasId(+tarefaId).subscribe(
        (tarefa: Tarefa) => {
          this.tarefa = { ...tarefa }
          this.form.patchValue(this.tarefa);
        },
        (error: any) => {
          this.showSpinner();
          this.rota.navigate([`/tarefas`]);
        },
        () => this.spinner.hide()
      );
    }
  }

}

