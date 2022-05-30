using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistroVentasWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace RegistroVentasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ClientesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Clientes
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Id, Nombre, Saldo, CantidadCompras from
                            dbo.Cliente
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

        // GET: api/Clientes/3
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            select Id, Nombre, Saldo, CantidadCompras from
                            dbo.Cliente
                            where Id = @ClienteId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClienteId", id);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }

            if (table.Rows.Count > 0)
            {
                return new JsonResult(new ClienteModel
                {
                    Id = table.Rows[0].Field<int>("Id"),
                    Nombre = table.Rows[0].Field<string>("Nombre"),
                    Saldo = table.Rows[0].Field<double>("Saldo"),
                    CantidadCompras = table.Rows[0].Field<int>("CantidadCompras")
                });
            }

            return new JsonResult(null);
        }

        // POST: api/Clientes
        [HttpPost]
        public void Post([FromBody] ClienteModel cliente)
        {
            string query = @"
                            insert into dbo.Cliente
                            values (@NombreCliente, @SaldoCliente, @CantidadComprasCliente)
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreCliente", cliente.Nombre);
                    command.Parameters.AddWithValue("@SaldoCliente", cliente.Saldo);
                    command.Parameters.AddWithValue("@CantidadComprasCliente", cliente.CantidadCompras);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // PUT: api/Clientes/3
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ClienteModel cliente)
        {
            string query = @"
                            update dbo.Cliente
                            set Nombre = @NombreCliente, Saldo = @SaldoCliente, CantidadCompras = @CantidadComprasCliente
                            where Id = @ClienteId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClienteId", id);
                    command.Parameters.AddWithValue("@NombreCliente", cliente.Nombre);
                    command.Parameters.AddWithValue("@SaldoCliente", cliente.Saldo);
                    command.Parameters.AddWithValue("@CantidadComprasCliente", cliente.CantidadCompras);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }

        // DELETE: api/Clientes/3
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string query = @"
                            delete from dbo.Cliente
                            where Id = @ClienteId
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConnectionDefault");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClienteId", id);
                    SqlDataReader dbReader = command.ExecuteReader();
                    table.Load(dbReader);
                    dbReader.Close();
                    connection.Close();
                }
            }
        }
    }
}
