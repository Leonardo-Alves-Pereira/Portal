namespace Portal.Comunicacao.Requisicao;

public class RequisicaoTarefaJson
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string DataCriacao { get; set; }
    public string DataConclusao { get; set; }
    public int UsuarioId { get; set; }
}
