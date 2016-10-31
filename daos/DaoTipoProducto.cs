using entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daos
{
    public class DaoTipoProducto
    {

        public static List<TipoProducto> obtenerTiposProducto()
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            
            //Entidades
            List<TipoProducto> listaTipos = new List<TipoProducto>();

            //Recupero los tipos de productos
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.ConnectionString = cadenaConexion;
                cmd.Connection = con;

                cmd.CommandText = @"SELECT id
                                          ,nombre
                                      FROM tipo_producto
                                     ORDER BY id";

                //Abre conexion y consulta
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                //Agrego tipos a la lista
                while(dr.Read())
                {
                    TipoProducto tipo = new TipoProducto();

                    tipo.Id = (int)dr["id"];
                    tipo.Nombre = dr["nombre"].ToString();

                    Debug.WriteLine(tipo.Id);
                    Debug.WriteLine(tipo.Nombre);

                    listaTipos.Add(tipo);
                }

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al recuperar los tipos de productos: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return listaTipos;
        }
    }
}
