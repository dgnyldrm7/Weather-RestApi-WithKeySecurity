using Microsoft.OpenApi.Models;
using Random_Weather_RestApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//was added swagger api test scheme!
builder.Services.AddSwaggerGen( x =>
{
    x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
       Description = "The Api key to access the api",
       Type = SecuritySchemeType.ApiKey,
       Name = "x-api-key",
       In = ParameterLocation.Header,
       Scheme = "ApiKeyScheme"
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        {scheme , new List<string>()  }
    };

    x.AddSecurityRequirement(requirement);
});


//ApiKey Filter was added here!
builder.Services.AddScoped<ApiKeyAuthFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Bunlarýn araya eklenecek! - Tüm controller'a key eklemesi yapar!
//app.UseMiddleware<ApiKeyAuthMiddleware>();



app.UseAuthorization();

app.MapControllers();

app.Run();
