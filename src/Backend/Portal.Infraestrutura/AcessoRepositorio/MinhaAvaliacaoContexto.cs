using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entidade;

namespace Portal.Infraestrutura.AcessoRepositorio;

public class PortalContexto : DbContext
{
    public PortalContexto(DbContextOptions<PortalContexto> options) : base(options) { }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefa { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortalContexto).Assembly);
    }
}
