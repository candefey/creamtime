using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using entidades;
using System.Data;

namespace daos
{
    public class DaoRol
    {
        public static Rol obtenerRolPorNombre(string nombre)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            Rol rol = new Rol();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM rol WHERE nombre LIKE @nombre";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    rol.Id = (int)dr["id"];
                    rol.Nombre = dr["nombre"].ToString();
                    rol.Descripcion = dr["descripcion"].ToString();
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

            return rol;
        }
    }
}
