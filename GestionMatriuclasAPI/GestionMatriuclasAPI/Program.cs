using GestionMatriuclasAPI.Models;
using GestionMatriuclasAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//recuperar la cadena de conexion
string conexion = builder.Configuration.GetConnectionString("conn");

// utilizar la cadena de conexion en el contexto del entity framework
builder.Services.AddDbContext<BdMatriculasContext>(
    opt => opt.UseSqlServer(conexion));

// Registrar el repositorio
builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
