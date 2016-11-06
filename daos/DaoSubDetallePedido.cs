using entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daos
{
    public class DaoSubDetallePedido
    {
        public static void insertarSubDetalle(SubDetallePedido subdetalle)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Inserto nuevo subdetalle
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                cmd.Connection = con;
                
                cmd.CommandText = @"INSERT INTO subdetalle_pedido (id_detalle_pedido
                                                                  ,id_producto)
                                    VALUES (@Detalle
                                           ,@Producto)";

                cmd.Parameters.AddWithValue("@Detalle", subdetalle.Detalle_Pedido.ID);
                cmd.Parameters.AddWithValue("@Producto", subdetalle.Producto.Id);

                //Inserto subdetalle y commit a la transaccion
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar un sudbetalle de pedido. " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }


    }
}
