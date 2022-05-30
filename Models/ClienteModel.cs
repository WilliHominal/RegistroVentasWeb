namespace RegistroVentasWeb.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public double Saldo { get; set; }

        public int CantidadCompras { get; set; }
    }
}
