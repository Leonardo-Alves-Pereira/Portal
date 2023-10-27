using Portal.Api.Filtros;
using Portal.Api.Middleware;
using Portal.Application;
using Portal.Application.Servicos.AutoMapper;
using Portal.Domain.Extension;
using Portal.Infraestrutura;
using Portal.Infraestrutura.Migrations;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddRouting(option => option.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Portal API", Version = "1.0" });
            option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "JWT Autorization header utilizando o Bearer scheme. Exemplo: \" Authorization: Bearer (token) \""
            });
            option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                        new string[] { }
                }

            });
        });
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
    }
}