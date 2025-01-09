using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories;
using LoveLetters.Repository.Repositories.Interfaces;
using LoveLetters.Service.Profiles;
using LoveLetters.Service.Services;
using LoveLetters.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LoveLettersContext>(options =>
            options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 21))
            )
        );

builder.Services.AddAutoMapper(typeof(MappingProfile));

#region services
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion

#region repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
#endregion

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
