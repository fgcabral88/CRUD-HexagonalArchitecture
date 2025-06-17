using CRUD_HexagonalArchitecture.Application.Ports.In;
using CRUD_HexagonalArchitecture.Application.UseCases.Tax;
using CRUD_HexagonalArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Database: SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 💡 Application Services
builder.Services.AddScoped<CRUD_HexagonalArchitecture.Application.Ports.Out.ITaxRepository, TaxRepository>();
builder.Services.AddScoped<ICreateTaxUseCase, CreateTaxUseCase>();
builder.Services.AddScoped<IGetTaxUseCase, GetTaxUseCase>();
builder.Services.AddScoped<IUpdateTaxUseCase, UpdateTaxUseCase>();
builder.Services.AddScoped<IDeleteTaxUseCase, DeleteTaxUseCase>();

// 📦 Controllers
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// 📘 Swagger com anotações e XML docs
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

// 🚀 Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
