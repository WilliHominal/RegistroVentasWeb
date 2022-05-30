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
    public class ProductosCompradosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductosCompradosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/ProductosComprados
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select VentaId, ProductoId, Cantidad, Costo from
                            dbo.ProductoComprado
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

        // GET: api/ProductosComprados/3 => Lista de productos comprados donde VentaId = id
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            select ProductoId, Cantidad, Costo from
                            dbo.ProductoComprado
                            where VentaId = @VentaId
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

            return new JsonResult(table);
        }

        // POST: api/ProductosComprados
        [HttpPost]
        public void Post([FromBody] ProductoCompradoModel productoComprado)
        {
            string query = @"
                            insert into dbo.ProductoComprado
                            values (@VentaId, @ProductoId, @Cantidad, @Costo)
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VentaId", productoComprado.VentaId);
                    command.Parameters.AddWithValue("@ProductoId", productoComprado.ProductoId);
                    command.Parameters.AddWithValue("@Cantidad", productoComprado.Cantidad);
                    command.Parameters.AddWithValue("@Costo", productoComprado.Costo);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // PUT: api/ProductosComprados/3/2
        [HttpPut("{idv}/{idp}")]
        public void Put(int idv, int idp, [FromBody] ProductoCompradoModel productoComprado)
        {
            string query = @"
                            update dbo.ProductoComprado
                            set Cantidad = @Cantidad, Costo = @Costo
                            where VentaId = @VentaId and ProductoId = @ProductoId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VentaId", idv);
                    command.Parameters.AddWithValue("@ProductoId", idp);
                    command.Parameters.AddWithValue("@Cantidad", productoComprado.Cantidad);
                    command.Parameters.AddWithValue("@Costo", productoComprado.Costo);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // DELETE: api/ProductosComprados/3/1
        [HttpDelete("{idv}/{idp}")]
        public void Delete(int idv, int idp)
        {
            string query = @"
                            delete from dbo.ProductoComprado
                            where VentaId = @VentaId and ProductoId = @ProductoId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VentaId", idv);
                    command.Parameters.AddWithValue("@ProductoId", idp);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }
    }
}
