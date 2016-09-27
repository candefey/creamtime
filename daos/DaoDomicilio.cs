using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using entidades;
using System.Data;
using System.Data.SqlClient;

namespace daos
{
    public class DaoDomicilio
    {
        public static Domicilio insertarDomicilio(Domicilio dom)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();


            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();

                string sql = "INSERT INTO domicilios (calle,numero,id_barrio) VALUES (@Calle,@Numero,@IdBarrio);SELECT @@Identity";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@Calle", dom.Calle);
                cmd.Parameters.AddWithValue("@Numero", dom.Numero);
                cmd.Parameters.AddWithValue("@IdBarrio", dom.Barrio.Id);

                int dom_id = (int)cmd.ExecuteScalar();
                dom.Id = dom_id;


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

            return dom;

        }
    }
}
