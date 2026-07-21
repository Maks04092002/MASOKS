using MasoksTech.Core;
using MasoksTech.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// --- 1. TRADUCTOR DE CONEXIÓN PARA RAILWAY ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgres"))
{
    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':');
    connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.LocalPath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Disable;";
}

// 2. Configurar la Base de Datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. Agregar servicios de Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 4. AUTO-CREACIÓN DE TABLAS Y USUARIOS SEMILLA (SEED) ---
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Crea la base de datos y sus tablas automáticamente en PostgreSQL
    dbContext.Database.EnsureCreated();

    // Inserta los usuarios iniciales de prueba si la tabla está vacía
    if (!dbContext.Usuarios.Any())
    {
        dbContext.Usuarios.AddRange(
            new Usuario 
            { 
                Username = "admin", 
                Password = "123", 
                NombreCompleto = "Administrador del Sistema", 
                Rol = "Admin" 
            },
            new Usuario 
            { 
                Username = "cliente", 
                Password = "123", 
                NombreCompleto = "Cliente de Prueba", 
                Rol = "Cliente" 
            }
        );
        dbContext.SaveChanges();
    }
}

// --- 5. HABILITAR SWAGGER TAMBIÉN EN PRODUCCIÓN ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasoksTech API v1");
    c.RoutePrefix = "swagger"; // Permite entrar en https://tu-app.up.railway.app/swagger
});

// --- 6. HABILITAR PÁGINAS WEB (wwwroot) ---
app.UseDefaultFiles(); 
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
