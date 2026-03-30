using UsuariosApi.Application.Services;
using UsuariosApi.Domain.Contracts;
using UsuariosApi.Infrastructure.Persistence;
using UsuariosApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoSettings = builder.Configuration
    .GetSection("MongoDbSettings")
    .Get<MongoDbSettings>() ?? new MongoDbSettings();

builder.Services.AddSingleton(mongoSettings);
builder.Services.AddSingleton<IUsuarioRepository, MongoUsuarioRepository>();
builder.Services.AddSingleton<MongoIndexInitializer>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<MongoIndexInitializer>();
    await initializer.CreateIndexesAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
