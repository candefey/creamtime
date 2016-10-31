using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidades;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace daos
{
    public class DaoUsuario
    {
        public static void insertarUsuario(Usuario user, SqlConnection con, SqlTransaction tran, int id_cliente)
        {

            SqlConnection cn = con;

            string key = "d9MlX2i99nSaXSbkusmxXquDUEZd61XV";
            user.Password = passwordEncrypt(user.Password, key);


            try
            {
                string sql = "";
                if (user.Id == null && user.ClienteId == null)
                {
                    sql = "INSERT INTO usuarios (username,password,id_persona) VALUES (@Username,@Password,@IdCliente);";
                }
                else
                {
                    if (user.Id != null && user.ClienteId == null )
                    {
                        sql = "UPDATE usuarios SET password=@Password,id_persona=@IdCliente;";
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@IdCliente", id_cliente);

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar cliente." + ex.Message);
            }

        }

        public static void actualizarUsuario(Usuario user, SqlConnection con, SqlTransaction tran, int id_cliente)
        {

            SqlConnection cn = con;
            try
            {

               string sql = "UPDATE usuarios SET username=@Username WHERE id_persona=@IdCliente;";
                

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@IdCliente", id_cliente);

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar cliente." + ex.Message);
            }

        }

        public static Usuario existeUsuario(Usuario usuario)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            Usuario user_query = new Usuario();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT id,username,id_persona FROM usuarios WHERE username LIKE @Username";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@username", usuario.Username));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    user_query.Id = (int)dr["id_persona"];
                    user_query.Username = dr["username"].ToString();
                    user_query.ClienteId = (int)dr["id_persona"];
                }

                dr.Close();
                
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

            return user_query;
        }

        public static Usuario login(Usuario usuario)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            string key = "d9MlX2i99nSaXSbkusmxXquDUEZd61XV";


            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM usuarios WHERE username LIKE @Username";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Username", usuario.Username));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string password = passwordDecrypt(dr["password"].ToString(), key);
                    if (usuario.Password == password)
                    {
                        usuario.ClienteId = (int)dr["id_persona"];
                    }
                }

                dr.Close();

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

            return usuario;
        }

        //Encrypting a string
        public static string passwordEncrypt(string inText, string key)
        {
            byte[] bytesBuff = Encoding.Unicode.GetBytes(inText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        cStream.Close();
                    }
                    inText = Convert.ToBase64String(mStream.ToArray());
                }
            }
            return inText;
        }
        //Decrypting a string
        public static string passwordDecrypt(string cryptTxt, string key)
        {
            cryptTxt = cryptTxt.Replace(" ", "+");
            byte[] bytesBuff = Convert.FromBase64String(cryptTxt);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        cStream.Close();
                    }
                    cryptTxt = Encoding.Unicode.GetString(mStream.ToArray());
                }
            }
            return cryptTxt;
        }

    }
}
