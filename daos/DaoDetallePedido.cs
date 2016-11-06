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
    public class DaoDetallePedido
    {
        public static void insertarDetalle(DetallePedido detalle)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Inserto nuevo producto
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                cmd.Connection = con;
                
                cmd.CommandText = @"INSERT INTO detalle_pedido (id_pedido
                                                               ,id_producto
                                                               ,cantidad
                                                               ,precio)
                                    VALUES (@Pedido
                                           ,@Producto
                                           ,@Cantidad
                                           ,@Precio); Select @@Identity;";

                cmd.Parameters.AddWithValue("@Pedido", detalle.Pedido.ID);
                cmd.Parameters.AddWithValue("@Producto", detalle.Producto.Id);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@Precio", detalle.Precio);

                //Inserto detalle del pedido
                int detalle_id = Convert.ToInt32(cmd.ExecuteScalar());
                
                //Inserto subdetalles del pedido
                foreach (SubDetallePedido sub in detalle.sabores)
                {
                    sub.Detalle_Pedido.ID = detalle_id;
                    DaoSubDetallePedido.insertarSubDetalle(sub);
                }

                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar un detalle de pedido. " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }


    }
}
