namespace Portal.Comunicacao.Requisicao;

public class RequisicaoTarefaJson
{
    public int Id { get; set; }
    public string Título { get; set; }
    public string Descrição { get; set; }
    public string DataConclusão { get; set; }
    public int UsuarioId { get; set; }
}
