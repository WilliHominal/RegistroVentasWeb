using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistroVentasWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace RegistroVentasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProveedoresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Proveedores
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Id, Nombre, Deuda from
                            dbo.Proveedor
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

        // GET: api/Proveedores/3
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            select Id, Nombre, Deuda from
                            dbo.Proveedor
                            where Id = @ProveedorId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProveedorId", id);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }

            if (table.Rows.Count > 0)
            {
                return new JsonResult(new ProveedorModel
                {
                    Id = table.Rows[0].Field<int>("Id"),
                    Nombre = table.Rows[0].Field<string>("Nombre"),
                    Deuda = table.Rows[0].Field<double>("Deuda"),
                });
            }

            return new JsonResult(null);
        }

        // POST: api/Proveedores
        [HttpPost]
        public void Post([FromBody] ProveedorModel proveedor)
        {
            string query = @"
                            insert into dbo.Proveedor
                            values (@NombreProveedor, @DeudaProveedor)
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreProveedor", proveedor.Nombre);
                    command.Parameters.AddWithValue("@DeudaProveedor", proveedor.Deuda);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // PUT: api/Proveedores/3
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProveedorModel proveedor)
        {
            string query = @"
                            update dbo.Proveedor
                            set Nombre = @NombreProveedor, Deuda = @DeudaProveedor
                            where Id = @ProveedorId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProveedorId", id);
                    command.Parameters.AddWithValue("@NombreProveedor", proveedor.Nombre);
                    command.Parameters.AddWithValue("@DeudaProveedor", proveedor.Deuda);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // DELETE: api/Proveedores/3
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string query = @"
                            delete from dbo.Proveedor
                            where Id = @ProveedorId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProveedorId", id);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }
    }
}
