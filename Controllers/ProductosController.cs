using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistroVentasWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace RegistroVentasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Productos
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Id, Nombre, ProveedorId, Precio, Stock from
                            dbo.Producto
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

        // GET: api/Productos/3
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            select Id, Nombre, ProveedorId, Precio, Stock from
                            dbo.Producto
                            where Id = @ProductoId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductoId", id);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }

            if (table.Rows.Count > 0)
            {
                return new JsonResult(new ProductoModel
                {
                    Id = table.Rows[0].Field<int>("Id"),
                    Nombre = table.Rows[0].Field<string>("Nombre"),
                    ProveedorId = table.Rows[0].Field<int>("ProveedorId"),
                    Precio = table.Rows[0].Field<double>("Precio"),
                    Stock = table.Rows[0].Field<double>("Stock"),
                });
            }

            return new JsonResult(null);
        }

        // POST: api/Productos
        [HttpPost]
        public void Post([FromBody] ProductoModel producto)
        {
            string query = @"
                            insert into dbo.Producto
                            values (@NombreProducto, @ProveedorIdProducto, @PrecioProducto, @StockProducto)
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreProducto", producto.Nombre);
                    command.Parameters.AddWithValue("@ProveedorIdProducto", producto.ProveedorId);
                    command.Parameters.AddWithValue("@PrecioProducto", producto.Precio);
                    command.Parameters.AddWithValue("@StockProducto", producto.Stock);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // PUT: api/Productos/3
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductoModel producto)
        {
            string query = @"
                            update dbo.Producto
                            set Nombre = @NombreProducto, ProveedorId = @ProveedorIdProducto, Precio = @PrecioProducto, Stock = @StockProducto
                            where Id = @ProductoId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductoId", id);
                    command.Parameters.AddWithValue("@NombreProducto", producto.Nombre);
                    command.Parameters.AddWithValue("@ProveedorIdProducto", producto.ProveedorId);
                    command.Parameters.AddWithValue("@PrecioProducto", producto.Precio);
                    command.Parameters.AddWithValue("@StockProducto", producto.Stock);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // DELETE: api/Productos/3
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string query = @"
                            delete from dbo.Producto
                            where Id = @ProductoId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductoId", id);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }
    }
}
