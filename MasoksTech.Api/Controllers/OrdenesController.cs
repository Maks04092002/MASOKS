using Microsoft.AspNetCore.Mvc;
using MasoksTech.Core;
using MasoksTech.Infrastructure;
using System.Linq;

namespace MasoksTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdenesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Registrar nueva compra
        [HttpPost]
        public IActionResult ProcesarCompra([FromBody] Orden nuevaOrden)
        {
            _context.Ordenes.Add(nuevaOrden);
            _context.SaveChanges();

            return Ok(new { mensaje = "Pago procesado exitosamente", ordenId = nuevaOrden.Id });
        }

        // GET: Listar todas las ventas para el Admin
        [HttpGet]
        public IActionResult ObtenerOrdenes()
        {
            var lista = _context.Ordenes.OrderByDescending(o => o.FechaCompra).ToList();
            return Ok(lista);
        }
    }
}