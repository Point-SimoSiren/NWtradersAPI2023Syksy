using Microsoft.EntityFrameworkCore;
using NWtradersAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injektion
builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("paikallinen")
    //  builder.Configuration.GetConnectionString("pilvi")
    ));



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
