using MasoksTech.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la Base de Datos PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Generador de OpenAPI/Swagger

var app = builder.Build();

// --- 1. CREACIÓN AUTOMÁTICA DE TABLAS EN POSTGRESQL ---
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configurar el pipeline de solicitudes HTTP (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- 2. HABILITAR ARCHIVOS ESTÁTICOS (DEBE IR ANTES DE AUTORIZACIÓN) ---
app.UseDefaultFiles(); // Busca index.html o login.html automáticamente
app.UseStaticFiles();  // Sirve los archivos de la carpeta wwwroot

// NOTA: Se comenta app.UseHttpsRedirection() para evitar el bloqueo en http://localhost:5107
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();