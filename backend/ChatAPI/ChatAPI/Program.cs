using AutoMapper;
using Chat.Infra.Contexts;
using Chat.Security;
using ChatAPI.Hubs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// database configuration
builder.Services.AddDbContext<ChatDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string profilesPath = String.Empty;
string settingsName = String.Empty; 
#if DEBUG
profilesPath = @"..\Chat\bin\Debug\net6.0\Chat.dll";
settingsName = "appsettings.Development.json";
#else
profilesPath = @"..\Chat\bin\Release\net6.0\Chat.dll";
settingsName = "appsettings.json";
#endif

// AutoMapper profiles injection
var assembly = Assembly.LoadFrom(profilesPath);
var profileTypes = assembly.GetTypes()
    .Where(type => typeof(Profile).IsAssignableFrom(type) && type != typeof(Profile))
    .ToArray();
if (profileTypes != null && profileTypes.Length > 0)
    builder.Services.AddAutoMapper(profileTypes);

// adds appsettings to configuration
builder.Configuration.AddJsonFile(settingsName, optional: false, reloadOnChange: true);
var config = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(config);

// inject configuration into token service
TokenService.Initialize(config);
// configure authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Secret"));
builder.Services.AuthenticationConfigure(key);

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
