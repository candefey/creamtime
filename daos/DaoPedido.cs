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

                cmd.Parameters.AddWithValue("@Pedido", pedido.Fecha_Pedido);
                cmd.Parameters.AddWithValue("@Cliente", pedido.Cliente.Id);
                cmd.Parameters.AddWithValue("@Monto", pedido.Monto);
                cmd.Parameters.AddWithValue("@Estado", pedido.Estado.ID);
                cmd.Parameters.AddWithValue("@NroPedido", pedido.Nro_Pedido);

                //Inserto pedido
                int pedido_id = Convert.ToInt32(cmd.ExecuteScalar());
                pedido.ID = pedido_id;

                //Inserto subdetalles del pedido
                foreach (DetallePedido det in detalles)
                {   
                    det.Pedido = pedido;
                    DaoDetallePedido.insertarDetalle(det, con, tran);
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


        public static List<Pedido> informePedidos(DateTime? desde, DateTime? hasta, int? estado, string apellido)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Entidades
            List<Pedido> listaPedidos = new List<Pedido>();
            Pedido pedido;
            Cliente cliente;
            Estado estadoPed;

            //Recupero los Productos
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.ConnectionString = cadenaConexion;
                cmd.Connection = con;

                cmd.CommandText = @"SELECT ped.id
                                          ,ped.nro_pedido
                                          ,ped.fecha_pedido
	                                      ,SUM(ped.monto) as Monto
	                                      ,est.id as idEstado
	                                      ,est.nombre as nomEstado
	                                      ,cli.nombre as nomCliente
	                                      ,cli.apellido	as apeCliente
                                      FROM pedido ped INNER JOIN personas cli
                                      ON ped.id_cliente = cli.id INNER JOIN detalle_pedido det
	                                  ON det.id_pedido = ped.id INNER JOIN estados est
	                                  ON ped.id_estado = est.id
                                      WHERE (ped.fecha_pedido BETWEEN ISNULL(@Desde, DATEADD(day, -1, ped.fecha_pedido))
		                                                          AND ISNULL(@Hasta, DATEADD(day, 1, ped.fecha_pedido)))
                                        AND (UPPER(cli.apellido) = UPPER(ISNULL(@Apellido, cli.apellido)))
                                        AND (ped.id_estado = ISNULL(@Estado, ped.id_estado))
                                     GROUP BY ped.nro_pedido, ped.fecha_pedido, ped.id, cli.nombre, cli.apellido, est.id, est.nombre";
                    
                cmd.Parameters.AddWithValue("@Desde", desde ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Hasta", hasta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", estado ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Apellido", apellido ?? (object)DBNull.Value);

                //Abre conexion y consulta
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                //Agrego los productos a la lista
                while (dr.Read())
                {
                    pedido = new Pedido();
                    cliente = new Cliente();
                    estadoPed = new Estado();

                    pedido.ID = (int)dr["id"];
                    pedido.Nro_Pedido = (long)dr["nro_pedido"];
                    pedido.Fecha_Pedido = (DateTime)dr["fecha_pedido"];
                    pedido.Fecha_Pedido = pedido.Fecha_Pedido.Date;
                    pedido.Monto = float.Parse(dr["Monto"].ToString());

                    estadoPed.ID = (int)dr["idEstado"];
                    estadoPed.Nombre = dr["nomEstado"].ToString();
                    pedido.Estado = estadoPed;

                    cliente.Nombre = dr["nomCliente"].ToString();
                    cliente.Apellido = dr["apeCliente"].ToString();
                    pedido.Cliente = cliente;

                    listaPedidos.Add(pedido);
                }

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al recuperar los pedidos: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return listaPedidos;
        }
    }    
}
