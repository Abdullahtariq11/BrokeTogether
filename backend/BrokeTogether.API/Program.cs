using BrokeTogether.API.Extension;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigurePosgresContext(builder.Configuration);
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
