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
    public class DaoSexo
    {
        public Sexo obtenerSexoPorNombre(string nombre)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            Sexo sexo = new Sexo();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM sexo WHERE nombre LIKE @nombre";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()!=false)
                {
                    sexo.Id = (int)dr["id"];
                    sexo.Nombre = dr["nombre"].ToString();
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

            return sexo;
        }
    }
}
