using entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daos
{
    public class DaoEstados
    {
        public static Estado obtenerEstadoPorNombre(string nombre)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            Estado estado = new Estado();
            try
            {

                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT e.id,e.nombre FROM estados e WHERE nombre LIKE @Nombre";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    estado.ID = (int)dr["id"];
                    estado.Nombre = dr["nombre"].ToString();
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

            return estado;
        }
    }
}
