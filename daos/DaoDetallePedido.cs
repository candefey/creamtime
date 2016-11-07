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
        public static void insertarDetalle(DetallePedido detalle, SqlConnection conexion, SqlTransaction transaction)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = conexion;

            //Inserto nuevo producto
            SqlCommand cmd = new SqlCommand();
            SqlTransaction tran = transaction;
            try
            {
                cmd.Connection = con;
                cmd.Transaction = tran;

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
                detalle.ID = detalle_id;

                //Inserto subdetalles del pedido
                foreach (SubDetallePedido sub in detalle.sabores)
                {   
                    sub.Detalle_Pedido = detalle;
                    DaoSubDetallePedido.insertarSubDetalle(sub, con, tran);
                }

                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar un detalle de pedido. " + ex.Message);
            }            
        }


    }
}
