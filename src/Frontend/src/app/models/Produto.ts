
export interface Produto {
  id: number;
  nome: string; 
  quantidade: number;
  preco: number;

  // modelos antigos de outro projeto meu, faltava adaptar.
  local: string; //local
  qtdPessoas: number; // qtdPessoas
  dataProduto?: Date;
  tema: string;
  imagemURL: string;
  telefone: string;
  email: string;
}
