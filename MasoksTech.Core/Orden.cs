namespace MasoksTech.Core
{
    public class Orden
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public decimal TotalCobrado { get; set; }
        public string MetodoPago { get; set; } = "Efectivo"; // Tarjeta, Yape, Transferencia, etc.
        public string EstadoPago { get; set; } = "PAGADO";
        public DateTime FechaCompra { get; set; } = DateTime.UtcNow;
    }
}