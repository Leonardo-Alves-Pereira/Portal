using Portal.Comunicacao.Requisicao;

namespace Portal.Comunicacao.Resposta;

public class RespostaTarefaJson
{
    public long Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string DataCriacao { get; set; }
    public string DataConclusao { get; set; }
    public int UsuarioId { get; set; }
    public RespostaUsuarioJson Usuario { get; set; }
}
