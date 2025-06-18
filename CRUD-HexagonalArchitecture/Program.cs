using CRUD_HexagonalArchitecture.Application.Ports.In;
using CRUD_HexagonalArchitecture.Application.UseCases.Tax;
using CRUD_HexagonalArchitecture.Domain.Interfaces.Repositories;
using CRUD_HexagonalArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adiciona variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

// Verifica se está rodando em container
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

var connectionString = isDocker
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : builder.Configuration.GetConnectionString("DefaultConnectionFromLocal");

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("Connection string está vazia. Verifique o docker-compose ou appsettings.");
    throw new InvalidOperationException("Connection string está vazia. Verifique o docker-compose ou appsettings.");
}
else
{
    Console.WriteLine($"Connection string carregada: {connectionString}");
}

// Configura o DbContext com SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registra casos de uso e repositórios
builder.Services.AddScoped<ICreateTaxUseCase, CreateTaxUseCase>();
builder.Services.AddScoped<IGetTaxUseCase, GetTaxUseCase>();
builder.Services.AddScoped<IUpdateTaxUseCase, UpdateTaxUseCase>();
builder.Services.AddScoped<IDeleteTaxUseCase, DeleteTaxUseCase>();
builder.Services.AddScoped<ITaxRepository, TaxRepository>();

// Configura controllers e JSON
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Configura Swagger com comentários XML
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CRUD Hexagonal Architecture",
        Version = "v1",
        Description = "API com arquitetura hexagonal para gerenciamento de impostos"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
