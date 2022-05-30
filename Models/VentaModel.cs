using System;

namespace RegistroVentasWeb.Models
{
    public class VentaModel
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public bool Fiado { get; set; }

        public DateTime Fecha { get; set; }

        public double Monto { get; set; }
    }
}
