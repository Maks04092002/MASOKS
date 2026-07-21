using MasoksTech.Core;
using MasoksTech.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasoksTech.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DtoLogin request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest(new { mensaje = "Ingrese usuario y contraseña." });

                // Búsqueda directa en la base de datos
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

                if (usuario == null)
                    return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });

                return Ok(new
                {
                    id = usuario.Id,
                    nombre = usuario.NombreCompleto ?? usuario.Username,
                    username = usuario.Username,
                    rol = usuario.Rol ?? "Cliente"
                });
            }
            catch (Exception ex)
            {
                // Captura el error de PostgreSQL y lo devuelve de forma entendible
                var detalle = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { mensaje = "Error de Base de Datos: " + detalle });
            }
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] Usuario nuevoUsuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nuevoUsuario.Username) || string.IsNullOrWhiteSpace(nuevoUsuario.Password))
                    return BadRequest(new { mensaje = "Todos los campos son obligatorios." });

                var existe = await _context.Usuarios.AnyAsync(u => u.Username == nuevoUsuario.Username);
                if (existe)
                    return BadRequest(new { mensaje = "El usuario ya se encuentra registrado." });

                if (string.IsNullOrEmpty(nuevoUsuario.Rol))
                    nuevoUsuario.Rol = "Cliente";

                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "¡Cuenta creada exitosamente! Ya puedes iniciar sesión." });
            }
            catch (Exception ex)
            {
                var detalle = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { mensaje = "Error de Base de Datos: " + detalle });
            }
        }
    }

    public class DtoLogin
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}