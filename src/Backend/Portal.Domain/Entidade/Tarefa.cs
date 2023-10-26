using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Domain.Entidade;

[Table("Tarefas")]
public class Tarefa :EntidadeBase
{
    public string Título { get; set; }
    public string Descrição { get; set; }
    public string DataConclusão { get; set; }
    public long UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}