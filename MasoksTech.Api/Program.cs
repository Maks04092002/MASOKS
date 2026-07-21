using MasoksTech.Core;
using MasoksTech.Infrastructure;
using Microsoft.EntityFrameworkCore;

// --- CORRECCIÓN ENOTIFY / LINUX RENDER ---
var builder = WebApplication.CreateBuilder(args);

// Desactivamos la recarga en caliente (reloadOnChange) para evitar saturar descriptores de archivo
builder.Configuration.Sources.Clear();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

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

// --- HABILITAR SWAGGER TAMBIÉN EN PRODUCCIÓN (RENDER) ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasoksTech API v1");
    c.RoutePrefix = "swagger"; // Permite entrar en https://tu-app.onrender.com/swagger
});

// --- 4. HABILITAR PÁGINAS WEB (wwwroot) ---
app.UseDefaultFiles(); 
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
