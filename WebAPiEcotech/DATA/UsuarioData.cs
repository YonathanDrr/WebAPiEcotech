using WebAPiEcotech.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebAPiEcotech.DATA
{

   
    public class UsuarioData
    {
        private readonly string? conexion;

        public UsuarioData(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("ConexionSQL");
        }

      


        public async Task<List<Usuario>> ListaUsuarios() 
        {
        
            List<Usuario> lista = new List<Usuario>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("spListarUsuarios", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync()) 
                {
                    while(await reader.ReadAsync()) 
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]), 
                            Email = Convert.ToString(reader["Email"]),
                            IdRol = Convert.ToInt32(reader["IdRol"]),
                            Nombre = Convert.ToString(reader["Nombre"])
                        });
                    }
                }
            }

            return lista;
        }


        public async Task<Usuario>ObtenerUsuarios(int _IdUsuario)
        {

            Usuario obj = new Usuario();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("spListarUsuarioId", con);
                cmd.Parameters.AddWithValue("@IdUsuario", _IdUsuario);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        obj = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            Email = Convert.ToString(reader["Email"]),
                            IdRol = Convert.ToInt32(reader["IdRol"]),
                            Nombre = Convert.ToString(reader["Nombre"])
                        };
                    }
                }
            }

            return obj;
        }


        public async Task<bool> CrearUsuario( Usuario usuario)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("spCrearUsuario", con);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Contrasenna", usuario.Contrasenna);
                cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception ex)
                {

                    respuesta = false;
                }
            }

            return respuesta;
        }



        public async Task<bool> EditarUsuario(Usuario usuario)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("spModificarUsuario", con);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Contrasenna", usuario.Contrasenna);
                cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception)
                {

                    respuesta = false;
                }
            }

            return respuesta;
        }



        public async Task<bool> EliminarUsuario(int IdUsuario)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("spEliminarUsuario", con);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);      
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception)
                {

                    respuesta = false;
                }
            }

            return respuesta;
        }
    }
}
