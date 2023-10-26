using Portal.Comunicacao.Requisicao;

namespace Portal.Comunicacao.Resposta;

public class RespostaTarefaJson
{
    public long Id { get; set; }
    public string Título { get; set; }
    public string Descrição { get; set; }
    public string DataConclusão { get; set; }
    public int UsuarioId { get; set; }
    public RespostaUsuarioJson Usuario { get; set; }
}
