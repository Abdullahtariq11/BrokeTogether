using BrokeTogether.API.Extension;
using BrokeTogether.Application.Service;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigurePosgresContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.BindJwt(builder.Configuration);
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddControllers();
builder.Services.CongigureServiceManager();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Optional: show XML comments if you want method/summary docs
    // var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml));

    // JWT bearer support in Swagger UI ("Authorize" button)
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter a valid JWT bearer token. Example: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BrokeTogether API",
        Version = "v1",
        Description = "MVP backend for shared expenses & shopping lists"
    });
});

var app = builder.Build();
// Swagger in all environments (handy during MVP)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BrokeTogether API v1");
    c.RoutePrefix = string.Empty; // Swagger as the homepage
});

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
