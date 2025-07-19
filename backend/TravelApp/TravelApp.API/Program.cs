using Microsoft.EntityFrameworkCore;
using TravelApp.API;
using TravelApp.API.Controllers;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.BusinessLogic.Services.Concretes;
using TravelApp.DataAccess;
using TravelApp.DataAccess.Models;
using TravelApp.DataAccess.Repositories.Abstractions;
using TravelApp.DataAccess.Repositories.Concretes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<TravelAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TravelAppDb")));

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddControllers();

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

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();