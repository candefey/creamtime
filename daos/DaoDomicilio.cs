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
        public static Domicilio insertarDomicilio(Domicilio dom, SqlConnection con, SqlTransaction tran)
        {
            SqlConnection cn = con;

            try
            {
            
                string sql = "INSERT INTO domicilios (calle,numero,id_barrio) VALUES (@Calle,@Numero,@IdBarrio);SELECT @@Identity;";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Calle", dom.Calle);
                cmd.Parameters.AddWithValue("@Numero", dom.Numero);
                cmd.Parameters.AddWithValue("@IdBarrio", dom.Barrio.Id);

                int dom_id = Convert.ToInt32(cmd.ExecuteScalar());
                dom.Id = dom_id;


            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar cliente." + ex.Message);
            }

            return dom;

        }

        public static List<Localidad> listarLocalidades()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            List<Localidad> locs = new List<Localidad>();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM localidades";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Localidad loc = new Localidad();
                    loc.Id = (int)dr["id"];
                    loc.Nombre = dr["nombre"].ToString();
                    locs.Add(loc);
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

            return locs;
        }

        public static List<Barrio> listarBarrios(int id)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            List<Barrio> bars = new List<Barrio>();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM barrios WHERE id_localidad=@Localidad";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Localidad", id));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Barrio bar = new Barrio();
                    bar.Id = (int)dr["id"];
                    bar.Nombre = dr["nombre"].ToString();
                    bars.Add(bar);   
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

            return bars;
        }
    }
}
