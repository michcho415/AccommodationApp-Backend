using Services;
using FluentValidation.AspNetCore;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;
using Common;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
builder.Services.AddSingleton<IConfiguration>(configuration);
var appSettings = configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterServices();

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.RegisterValidators();

builder.Services.AddDbContextFactory<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Default", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader();
        policy.WithOrigins("https://localhost:4200").AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Default");

app.UseHttpsRedirection();

app.UseAuthorization();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
