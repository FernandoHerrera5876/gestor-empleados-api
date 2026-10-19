using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class Proovedor2Data
    {
        private readonly string conexion;

        public Proovedor2Data(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Proovedor2>> Lista()
        {
            List<Proovedor2> lista = new List<Proovedor2>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.Proovedor_ConsultarTodos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Proovedor2
                        {
                            ID = Convert.ToInt64(reader["Id"]),
                            ACTIVO = Convert.ToBoolean(reader["Activo"]),
                            CODIGO = reader["Codigo"].ToString(),
                            RAZON_SOCIAL = reader["RazonSocial"].ToString(),
                            RFC = reader["RFC"].ToString(),
                            CORREO = reader["Correo"].ToString(),
                            TELEFONO = reader["Telefono"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<bool> Crear(Proovedor2 objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("dbo.Proovedor_Insertar", con);
                cmd.Parameters.AddWithValue("@Activo", objeto.ACTIVO);
                cmd.Parameters.AddWithValue("@Codigo", objeto.CODIGO);
                cmd.Parameters.AddWithValue("@RazonSocial", objeto.RAZON_SOCIAL);
                cmd.Parameters.AddWithValue("@RFC", objeto.RFC);
                cmd.Parameters.AddWithValue("@Correo", objeto.CORREO);
                cmd.Parameters.AddWithValue("@Telefono", objeto.TELEFONO);
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

        public async Task<Proovedor2> Obtener(long ID)
        {
            Proovedor2 objeto = new Proovedor2();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.Proovedor_ConsultarPorId", con);
                cmd.Parameters.AddWithValue("@Id", ID);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Proovedor2
                        {
                            ID = Convert.ToInt64(reader["ID"]),
                            ACTIVO = Convert.ToBoolean(reader["ACTIVO"]),
                            CODIGO = reader["CODIGO"].ToString(),
                            RAZON_SOCIAL = reader["RAZON_SOCIAL"].ToString(),
                            RFC = reader["RFC"].ToString(),
                            CORREO = reader["CORREO"].ToString(),
                            TELEFONO = reader["TELEFONO"].ToString(),
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Editar(Proovedor2 objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("dbo.Proovedor_Actualizar", con);
                cmd.Parameters.AddWithValue("@Id", objeto.ID);
                cmd.Parameters.AddWithValue("@Activo", objeto.ACTIVO);
                cmd.Parameters.AddWithValue("@Codigo", objeto.CODIGO);
                cmd.Parameters.AddWithValue("@razonSocial", objeto.RAZON_SOCIAL);
                cmd.Parameters.AddWithValue("@Rfc", objeto.RFC);
                cmd.Parameters.AddWithValue("@Correo", objeto.CORREO);
                cmd.Parameters.AddWithValue("@Telefono", objeto.TELEFONO);
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

                SqlCommand cmd = new SqlCommand("dbo.Proovedor_Eliminar", con);
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
