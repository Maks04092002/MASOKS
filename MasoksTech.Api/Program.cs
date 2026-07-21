using MasoksTech.Core;
using MasoksTech.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de la Base de Datos PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Agregar servicios de Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 3. AUTO-CREACIÓN DE TABLAS Y USUARIOS SEMILLA (SEED) ---
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

// Configuración del entorno de desarrollo (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- 4. HABILITAR PÁGINAS WEB (wwwroot) ---
// UseDefaultFiles SIEMPRE va antes de UseStaticFiles
app.UseDefaultFiles(); 
app.UseStaticFiles();

// Se deshabilita la redirección forzada a HTTPS para evitar bloqueos en http://localhost:5107
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
