using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using entidades;

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
                string sql = "SELECT id FROM rol WHERE nombre LIKE @nombre";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() != false)
                {
                    rol.id = (int)dr["id"];
                    rol.nombre = dr["nombre"].ToString();
                    rol.descripcion = dr["descripcion"].ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return rol;
        }
    }
}
