using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Domain.Entidade;

[Table("Tarefas")]
public class Tarefa :EntidadeBase
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string DataConclusao { get; set; }
    public long UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}