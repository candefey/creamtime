using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidades;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace daos
{
    public class DaoCompras
    {
        public static List<Compra> listarCompraPorProveedor(int idPro)
        {

            List<Compra> compras = new List<Compra>();
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;


            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string consulta = "SELECT c.id AS idCompra, c.fecha_compra AS fecha, c.monto AS mon, c.nro_compra AS nro FROM compras c INNER JOIN detalle_compra dc ON dc.id_compra=c.id WHERE dc.id_proveedor=@idFiltro";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = consulta;
                cmd.Parameters.Add(new SqlParameter("@idFiltro", idPro));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool flag = false;
                    Compra c = new Compra();
                    c.Id = (int)reader["idCompra"];
                    c.Fecha = (DateTime)reader["fecha"];
                    c.Monto= float.Parse((reader["mon"]).ToString());
                    c.Nro=Convert.ToInt64(reader["nro"]);
                    foreach (var item in compras)
                    {
                        if (item.Id == c.Id)
                        {
                            flag = true;
                        }
                       
                    }
                    if(flag==false)
                        compras.Add(c);
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

            return compras;

        }

        public static List<Compra> listarCompraPorMp(int idMp)
        {

            List<Compra> compras = new List<Compra>();
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;


            SqlConnection con = new SqlConnection();
            try
            {
                
                con.ConnectionString = cadenaConexion;
                con.Open();
                string consulta = "SELECT c.id AS idCompra, c.fecha_compra AS fecha, c.monto AS mon, c.nro_compra AS nro FROM compras c INNER JOIN detalle_compra dc ON dc.id_compra=c.id WHERE dc.id_materia_prima=@idFiltro";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = consulta;
                cmd.Parameters.Add(new SqlParameter("@idFiltro", idMp));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool flag = false;
                    Compra c = new Compra();
                    c.Id = (int)reader["idCompra"];
                    c.Fecha = (DateTime)reader["fecha"];
                    c.Monto = float.Parse((reader["mon"]).ToString());
                    c.Nro = Convert.ToInt64(reader["nro"]);
                    foreach (var item in compras)
                    {
                        if (item.Id == c.Id)
                        {
                            flag = true;
                        }

                    }
                    if (flag == false)
                        compras.Add(c);
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

            return compras;

        }

        public static List<Compra> listarCompraMonto(float monto_desde, float monto_hasta)
        {

            List<Compra> compras = new List<Compra>();
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;


            SqlConnection con = new SqlConnection();
            try
            {

                con.ConnectionString = cadenaConexion;
                con.Open();
                string consulta = "SELECT c.id AS idCompra, c.fecha_compra AS fecha, c.monto AS mon, c.nro_compra AS nro FROM compras c WHERE c.monto>=@monto_desde AND c.monto<=@monto_hasta";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = consulta;
                cmd.Parameters.Add(new SqlParameter("@monto_desde", monto_desde));
                cmd.Parameters.Add(new SqlParameter("@monto_hasta", monto_hasta));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool flag = false;
                    Compra c = new Compra();
                    c.Id = (int)reader["idCompra"];
                    c.Fecha = (DateTime)reader["fecha"];
                    c.Monto = float.Parse((reader["mon"]).ToString());
                    c.Nro = Convert.ToInt64(reader["nro"]);
                    foreach (var item in compras)
                    {
                        if (item.Id == c.Id)
                        {
                            flag = true;
                        }

                    }
                    if (flag == false)
                        compras.Add(c);
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

            return compras;

        }
    }
}
