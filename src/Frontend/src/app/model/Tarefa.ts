import { RespostaUsuarioJson } from "./RespostaUsuarioJson";

export interface Tarefa {
    id: number;
    titulo: string;
    descricao: string;
    dataCriacao: Date;
    dataConclusao?: string;
    usuarioId: number;
    usuario: RespostaUsuarioJson;
}
