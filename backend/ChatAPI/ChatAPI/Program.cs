using AutoMapper;
using Chat.Domain.Models;
using Chat.Infra.Contexts;
using ChatAPI.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ChatDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string profilesPath = String.Empty;
#if DEBUG
profilesPath = @"..\Chat\bin\Debug\net6.0\Chat.dll";
#else
profilesPath = @"..\Chat\bin\Release\net6.0\Chat.dll";
#endif
Console.WriteLine(profilesPath);
var assembly = Assembly.LoadFrom(profilesPath);
var profileTypes = assembly.GetTypes()
    .Where(type => typeof(Profile).IsAssignableFrom(type) && type != typeof(Profile))
    .ToArray();
if (profileTypes != null && profileTypes.Length > 0)
    builder.Services.AddAutoMapper(profileTypes);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<ChatHub>("/chat");

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
