namespace RegistroVentasWeb.Models
{
    public class ProductoCompradoModel
    {
        public int VentaId { get; set; }

        public int ProductoId { get; set; }

        public double Cantidad { get; set; }

        public double Costo { get; set; }
    }
}
