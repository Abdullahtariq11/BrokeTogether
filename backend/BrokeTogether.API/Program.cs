using BrokeTogether.API.Extension;
using BrokeTogether.Application.Service;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigurePosgresContext(builder.Configuration);
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.BindJwt(builder.Configuration);
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddControllers();
builder.Services.CongigureServiceManager();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
