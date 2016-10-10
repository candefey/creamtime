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
        public static Usuario insertarUsuario(Usuario user, SqlConnection con, SqlTransaction tran)
        {

            SqlConnection cn = con;

            byte[] data = System.Text.Encoding.ASCII.GetBytes(user.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            user.Password = hash;
            

            try
            {

                string sql = "INSERT INTO usuarios (username,password) VALUES (@Username,@Password);Select @@Identity;";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                int user_id = Convert.ToInt32(cmd.ExecuteScalar());
                user.Id = user_id;


            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar cliente." + ex.Message);
            }

            return user;

        }

        public static Boolean existeUsuario(Usuario usuario)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            Boolean flag = false;
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM usuarios WHERE username LIKE @Username";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@username", usuario.Username));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    flag = true;
                }
                
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("" + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return flag;
        }
    
    }
}
