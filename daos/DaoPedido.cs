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
    public class DaoPedido
    {
        public void insertarPedido(Pedido pedido, List<DetallePedido> detalles)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Inserto nuevo producto
            SqlCommand cmd = new SqlCommand();
            SqlTransaction tran = null;
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                tran = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = tran;

                cmd.CommandText = @"INSERT INTO pedido (fecha_pedido
                                                       ,id_cliente
                                                       ,monto
                                                       ,id_estado
                                                       ,nro_pedido)
                                    VALUES (@Pedido
                                           ,@Cliente
                                           ,@Monto
                                           ,@Estado
                                           ,@NroPedido); Select @@identity;";

                cmd.Parameters.AddWithValue("@Pedido", DateTime.Now);
                cmd.Parameters.AddWithValue("@Cliente", pedido.Cliente.Id);
                cmd.Parameters.AddWithValue("@Monto", pedido.Monto);
                cmd.Parameters.AddWithValue("@Estado", pedido.Estado.ID);
                cmd.Parameters.AddWithValue("@NroPedido", pedido.Nro_Pedido);

                //Inserto pedido
                int pedido_id = Convert.ToInt32(cmd.ExecuteScalar());

                //Inserto subdetalles del pedido
                foreach (DetallePedido det in detalles)
                {
                    det.Pedido.ID = pedido_id;
                    DaoDetallePedido.insertarDetalle(det);
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    tran.Rollback();

                throw new ApplicationException("Error al insertar un pedido. " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

    }    
}
