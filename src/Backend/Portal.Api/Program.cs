using Portal.Api.Filtros;
using Portal.Api.Middleware;
using Portal.Application;
using Portal.Application.Servicos.AutoMapper;
using Portal.Domain.Extension;
using Portal.Infraestrutura;
using Portal.Infraestrutura.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraestrutura(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddMvc(option => option.Filters.Add(typeof(FiltroException)));

builder.Services.AddAutoMapper(typeof(AutoMapperConfiguracao));

builder.Services.AddScoped<UsuarioAutenticadoAttribute>();
builder.Services.AddScoped<LoginAttribute>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AtualizarDatabase();

app.UseMiddleware<CultureMiddleware>();
app.UseMiddleware<FiltroJwtMiddleware>();


app.Run();
                    
void AtualizarDatabase()
{
    Database.CriarDatabase(builder.Configuration.GetConexao(),
                           builder.Configuration.GetNomeDatabase());
    app.MigrationBancoDeDados();
}