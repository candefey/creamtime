using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidades;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace daos
{
    public class DaoUsuario
    {
        public static Usuario insertarUsuario(Usuario user)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(user.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            user.Password = hash;

            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();

                string sql = "INSERT INTO usuario (username,password) VALUES (@Username,@Password);SELECT @@Identity";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                int usuario_id = (int)cmd.ExecuteScalar();
                user.Id = usuario_id;


            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar cliente." + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return user;

        }
    
    }
}
