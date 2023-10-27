import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Component, OnInit, TemplateRef } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { Produtoservice } from '@app/services/produto.service';
import { Produto } from '@app/models/Produto';
import { DatePipe } from '@angular/common';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-produto-detalhe',
  templateUrl: './produto-detalhe.component.html',
  styleUrls: ['./produto-detalhe.component.scss'],
  providers: [DatePipe],
})
export class ProdutoDetalheComponent implements OnInit {
  modalRef: BsModalRef;
  produtoId: number;
  produto = {} as Produto;
  form: FormGroup;
  estadoSalvar = 'post';
  categoriaAtual = { id: 0, nome: '', indice: 0 };
  imagemURL = 'assets/img/upload.png';
  file: File;

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  get categorias(): FormArray {
    return this.form.get('categorias') as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private Produtoservice: Produtoservice,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router,
    private datePipe: DatePipe
  ) {
    this.localeService.use('pt-br');
  }

  public carregarProduto(): void {
    this.produtoId = +this.activatedRouter.snapshot.paramMap.get('id');

    if (this.produtoId !== null && this.produtoId !== 0) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.Produtoservice
        .getProdutoById(this.produtoId)
        .subscribe(
          (produto: Produto) => {
            this.produto = { ...produto };
            this.form.patchValue(this.produto);
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar carregar Produto.', 'Erro!');
            console.error(error);
          }
        )
        .add(() => this.spinner.hide());
    }
  }

  ngOnInit(): void {
    this.carregarProduto();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      local: ['', Validators.required],
      dataProduto: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: [''],
      categorias: this.fb.array([]),
    });
  }

  public mudarValorData(value: Date, indice: number, campo: string): void {
    this.categorias.value[indice][campo] = value;
  }

  public retornaTituloCategoria(nome: string): string {
    return nome === null || nome === '' ? 'Nome do categoria' : nome;
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarProduto(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.produto =
        this.estadoSalvar === 'post'
          ? { ...this.form.value }
          : { id: this.produto.id, ...this.form.value };

      this.Produtoservice[this.estadoSalvar](this.produto).subscribe(
        (produtoRetorno: Produto) => {
          this.toastr.success('Produto salvo com Sucesso!', 'Sucesso');
          this.router.navigate([`Produtos/detalhe/${produtoRetorno.id}`]);
        },
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toastr.error('Error ao salvar produto', 'Erro');
        },
        () => this.spinner.hide()
      );
    }
  }

  public salvarCategorias(): void {
    if (this.form.controls.categorias.valid) {
      this.spinner.show();

    }


  }

}
