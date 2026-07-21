using MasoksTech.Core;
using MasoksTech.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace MasoksTech.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CrearProducto([FromBody] Producto nuevoProducto)
    {
        // 1. Validar reglas de negocio (Escenario 2)
        if (nuevoProducto.Precio <= 0 || nuevoProducto.Stock < 0)
        {
            return BadRequest("El precio debe ser mayor a 0 y el stock no puede ser negativo.");
        }

        // 2. Guardar en la base de datos
        _context.Productos.Add(nuevoProducto);
        _context.SaveChanges();

        // 3. Retornar éxito (Escenario 1)
        return CreatedAtAction(nameof(CrearProducto), new { id = nuevoProducto.Id }, nuevoProducto);
    }
    [HttpGet]
    public IActionResult ObtenerProductos()
    {
        var productos = _context.Productos.ToList();
        return Ok(productos);
    }
    [HttpGet("{id}")]
    public IActionResult ObtenerProductoPorId(int id)
    {
        // Buscamos el producto por su llave primaria (ID)
        var producto = _context.Productos.Find(id);

        // Si no existe, cumplimos la especificación devolviendo 404
        if (producto == null)
        {
            return NotFound($"No se encontró el producto con el ID {id}.");
        }

        // Si existe, lo devolvemos con un 200 OK
        return Ok(producto);
    }
    [HttpPut("{id}")]
    public IActionResult ActualizarProducto(int id, [FromBody] Producto productoActualizado)
    {
        if (id != productoActualizado.Id) 
        {
            return BadRequest("El ID de la URL no coincide con el ID del producto.");
        }

        var producto = _context.Productos.Find(id);
        if (producto == null) 
        {
            return NotFound($"No se encontró el producto con el ID {id}.");
        }

        // Actualizamos los campos
        producto.Nombre = productoActualizado.Nombre;
        producto.Precio = productoActualizado.Precio;
        producto.Stock = productoActualizado.Stock;

        _context.SaveChanges();
        return NoContent(); // 204
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarProducto(int id)
    {
        var producto = _context.Productos.Find(id);
        if (producto == null) 
        {
            return NotFound($"No se encontró el producto con el ID {id}.");
        }

        _context.Productos.Remove(producto);
        _context.SaveChanges();

        return NoContent(); // 204
    }
}