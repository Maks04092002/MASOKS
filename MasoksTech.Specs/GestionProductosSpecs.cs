using MasoksTech.Core;
using MasoksTech.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasoksTech.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return Ok(producto);
        }

        // Método adaptado exactamente para la prueba unitaria (ActualizarProducto)
        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(int id, [FromBody] Producto producto)
        {
            var prodExistente = _context.Productos.Find(id);
            if (prodExistente == null) 
                return NotFound();

            prodExistente.Nombre = producto.Nombre;
            prodExistente.Precio = producto.Precio;
            prodExistente.Stock = producto.Stock;

            _context.SaveChanges();

            return NoContent(); // Retorna HTTP 204 NoContentResult exigido por la prueba
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) 
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Producto eliminado" });
        }
    }
}