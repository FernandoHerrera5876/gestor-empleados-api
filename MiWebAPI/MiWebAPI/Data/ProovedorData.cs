using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProovedorData
    {
        private readonly string conexion;

        public ProovedorData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Proovedor>> Lista()
        {
            List<Proovedor> lista = new List<Proovedor>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PROOVEDOR_SELECCIONA", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Proovedor
                        {
                            ID = Convert.ToInt64(reader["Id"]),
                            NOMBRE = reader["Nombre"].ToString(),
                            APELLIDO_PATERNO = reader["ApellidoPaterno"].ToString(),
                            APELLIDO_MATERNO = reader["ApellidoMaterno"].ToString(),
                            RFC = reader["Rfc"].ToString(),
                            CORREO = reader["Correo"].ToString(),
                            CREDITO = Convert.ToDecimal(reader["Credito"]),
                            NOMBRE_BANCO = reader["NombreBanco"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<bool> Crear(Proovedor objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("dbo.PROOVEDOR_AGREGA", con);
                cmd.Parameters.AddWithValue("@Nombre", objeto.NOMBRE);
                cmd.Parameters.AddWithValue("@Apellido_Paterno", objeto.APELLIDO_PATERNO);
                cmd.Parameters.AddWithValue("@Apellido_Materno", objeto.APELLIDO_MATERNO);
                cmd.Parameters.AddWithValue("@Rfc", objeto.RFC);
                cmd.Parameters.AddWithValue("@Correo", objeto.CORREO);
                cmd.Parameters.AddWithValue("@Credito", objeto.CREDITO);
                cmd.Parameters.AddWithValue("@Nombre_Banco", objeto.NOMBRE_BANCO);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<Proovedor> Obtener(long ID)
        {
            Proovedor objeto = new Proovedor();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PROOVEDOR_SELECCIONA_POR_ID", con);
                cmd.Parameters.AddWithValue("@Id", ID);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Proovedor
                        {
                            ID = Convert.ToInt64(reader["ID"]),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            APELLIDO_PATERNO = reader["APELLIDO_PATERNO"].ToString(),
                            APELLIDO_MATERNO = reader["APELLIDO_MATERNO"].ToString(),
                            RFC = reader["RFC"].ToString(),
                            CORREO = reader["CORREO"].ToString(),
                            CREDITO = Convert.ToDecimal(reader["CREDITO"]),
                            NOMBRE_BANCO = reader["NOMBRE_BANCO"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Editar(Proovedor objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("dbo.PROOVEDOR_ACTUALIZA", con);
                cmd.Parameters.AddWithValue("@Id", objeto.ID);
                cmd.Parameters.AddWithValue("@Nombre", objeto.NOMBRE);
                cmd.Parameters.AddWithValue("@Apellido_Paterno", objeto.APELLIDO_PATERNO);
                cmd.Parameters.AddWithValue("@Apellido_Materno", objeto.APELLIDO_MATERNO);
                cmd.Parameters.AddWithValue("@Rfc", objeto.RFC);
                cmd.Parameters.AddWithValue("@Correo", objeto.CORREO);
                cmd.Parameters.AddWithValue("@Credito", objeto.CREDITO);
                cmd.Parameters.AddWithValue("@Nombre_Banco", objeto.NOMBRE_BANCO);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(long ID)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("dbo.PROOVEDOR_ELIMINA", con);
                cmd.Parameters.AddWithValue("@Id", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}