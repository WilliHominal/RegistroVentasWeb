using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistroVentasWeb.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RegistroVentasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VentasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Ventas
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Id, ClienteId, Fiado, Fecha, Monto from
                            dbo.Venta
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(table);
        }

        // GET: api/Ventas/3
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            select Id, ClienteId, Fiado, Fecha, Monto from
                            dbo.Venta
                            where Id = @VentaId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VentaId", id);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }

            if (table.Rows.Count > 0)
            {
                return new JsonResult(new VentaModel
                {
                    Id = table.Rows[0].Field<int>("Id"),
                    ClienteId = table.Rows[0].Field<int>("ClienteId"),
                    Fiado = table.Rows[0].Field<bool>("Fiado"),
                    Fecha = table.Rows[0].Field<DateTime>("Fecha"),
                    Monto = table.Rows[0].Field<double>("Monto")
                });
            }

            return new JsonResult(null);
        }

        // POST: api/Ventas
        [HttpPost]
        public void Post([FromBody] VentaModel venta)
        {
            string query = @"
                            insert into dbo.Venta
                            values (@ClienteId, @Fiado, @Fecha, @Monto)
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClienteId", venta.ClienteId);
                    command.Parameters.AddWithValue("@Fiado", venta.Fiado);
                    command.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    command.Parameters.AddWithValue("@Monto", venta.Monto);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // PUT: api/Ventas/3
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] VentaModel venta)
        {
            string query = @"
                            update dbo.Venta
                            set ClienteId = @ClienteId, Fiado = @Fiado, Fecha = @Fecha, Monto = @Monto
                            where Id = @VentaId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VentaId", id);
                    command.Parameters.AddWithValue("@ClienteId", venta.ClienteId);
                    command.Parameters.AddWithValue("@Fiado", venta.Fiado);
                    command.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    command.Parameters.AddWithValue("@Monto", venta.Monto);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // DELETE: api/Ventas/3
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string query = @"
                            delete from dbo.Venta
                            where Id = @VentaId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VentaId", id);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }
    }
}
