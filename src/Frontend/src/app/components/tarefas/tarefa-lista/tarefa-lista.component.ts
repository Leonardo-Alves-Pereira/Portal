import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Tarefa } from 'src/app/model/Tarefa';
import { TarefaService } from 'src/app/service/tarefa.service';

@Component({
  selector: 'app-tarefa-lista',
  templateUrl: './tarefa-lista.component.html',
  styleUrls: ['./tarefa-lista.component.scss']
})
export class TarefaListaComponent implements OnInit {
  modalRef?: BsModalRef;
  message = '';
  tarefa!: Tarefa;
  public tarefasFiltradas: Tarefa[] = [];
  public tarefas: Tarefa[] = [];
  id: number = 0;
  private _filtroLista = '';

  constructor(private tarefaService: TarefaService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService) { }

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.tarefasFiltradas = this.filtroLista ? this.filtrarTarefas(this.filtroLista) : this.tarefas;
  }

  public ngOnInit(): void {
    this.listarTarefas();
  }

  public listarTarefas(): void {
    this.tarefaService.listarTarefas().subscribe({
      next: (tarefas: Tarefa[]) => {
        this.tarefas = tarefas;
        this.tarefasFiltradas = this.tarefas;
        this.spinner.show();
      },
      error: (error: any) => {
        this.toastr.error(error.error.mensagens, 'Erro', {
          timeOut: 10000
        });
        this.showSpinner();
      },
      complete: () => this.spinner.hide()
    });
  }

  public deletarTarefa(id: number): void {

    if (id !== 0) {
      this.tarefaService.deletarTarefa(id).subscribe(
       () => {
        this.listarTarefas();
       },
       (error: any) => {
        this.toastr.error('Erro na conexão!', 'Erro', {
          timeOut: 10000
        });
        this.showSpinner();
       },
        () => this.spinner.hide()  
      );
    }
  }

  public filtrarTarefas(filtro: string): Tarefa[] {
    filtro = filtro.toLocaleLowerCase();
    return this.tarefas.filter((tarefa: Tarefa) =>
      tarefa.usuario.nome.toLocaleLowerCase().includes(filtro) ||
      tarefa.titulo.toLocaleLowerCase().includes(filtro) ||
      tarefa.descricao.toLocaleLowerCase().includes(filtro)
    );
  }

  openModal(template: TemplateRef<any>, id: number) {
    this.id = id;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.deletarTarefa(this.id);
    this.modalRef?.hide();
    this.toastr.success('Tarefa excluída com sucesso!', 'Exclusão');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  showSpinner() {
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
    }, 5000);
  }

  detlheTarefa(id : number){
    this.router.navigate([`/tarefas/detalhe/${id}`]);
  }

}
