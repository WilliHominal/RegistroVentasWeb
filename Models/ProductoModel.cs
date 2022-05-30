namespace RegistroVentasWeb.Models
{
    public class ProductoModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int ProveedorId { get; set; }

        public double Precio { get; set; }

        public double Stock { get; set; }
    }
}
