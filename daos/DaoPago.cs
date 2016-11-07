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
    public static class DaoPago
    {
        public static List<PedidoEnvioView> obtenerPedidosPendientes()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            List<PedidoEnvioView> pedidoenvios = new List<PedidoEnvioView>();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT p.nro_pedido,p.fecha_pedido,p.monto,c.nombre AS 'clientenombre',c.apellido AS 'clienteapellido',d.calle,d.numero,b.nombre AS 'barrionombre',l.nombre AS 'localidadnombre',e.nombre AS 'estadonombre' FROM pedido p INNER JOIN";
                sql += " personas c ON p.id_cliente=c.id INNER JOIN domicilios d ON d.id=c.id_domicilio INNER JOIN barrios b ON d.id_barrio=b.id INNER JOIN";
                sql += " localidades l ON l.id=b.id_localidad INNER JOIN estados e ON p.id_estado=e.id WHERE e.nombre LIKE 'Enviado' OR e.nombre LIKE 'Local'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PedidoEnvioView pedidoenvio = new PedidoEnvioView();

                    pedidoenvio.NumeroPedido = (long)dr["nro_pedido"];
                    pedidoenvio.NombreCliente = dr["clientenombre"].ToString();
                    pedidoenvio.ApellidoCliente = dr["clienteapellido"].ToString();
                    pedidoenvio.Calle = dr["calle"].ToString();
                    pedidoenvio.Numero = dr["numero"].ToString();
                    pedidoenvio.Barrio = dr["barrionombre"].ToString();
                    pedidoenvio.Localidad = dr["localidadnombre"].ToString();
                    pedidoenvio.Estado = dr["estadonombre"].ToString();
                    pedidoenvio.FechaPedido = (DateTime)dr["fecha_pedido"];
                    pedidoenvio.Monto = float.Parse((dr["monto"]).ToString());

                    pedidoenvios.Add(pedidoenvio);

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

            return pedidoenvios;
        }

        public static void registrarPagoLocal(int id,int id_estado)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();
            DateTime fecha_pago = DateTime.Now;

            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();

                string sql2 = "UPDATE pedido SET id_estado=@IdEstado,fecha_pago=@FechaPago WHERE id=@IdPedido";

                SqlCommand cmd2 = new SqlCommand();


                cmd2.CommandText = sql2;
                cmd2.Connection = cn;


                cmd2.Parameters.AddWithValue("@IdEstado", id_estado);
                cmd2.Parameters.AddWithValue("@IdPedido", id);
                cmd2.Parameters.AddWithValue("@FechaPago", fecha_pago);


                cmd2.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)

                throw new ApplicationException("Error al insertar cliente." + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public static void registrarPagoDelivery(int id, int id_estado)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();
            SqlTransaction tran = null;
            DateTime fecha_pago = DateTime.Now;

            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();
                tran = cn.BeginTransaction();

                string sql = "UPDATE envios SET id_estado=@IdEstado WHERE id_pedido=@IdPedido";
                string sql2 = "UPDATE pedido SET id_estado=@IdEstado,fecha_pago=@FechaPago WHERE id=@IdPedido";

                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;

                cmd2.CommandText = sql2;
                cmd2.Connection = cn;
                cmd2.Transaction = tran;

                cmd.Parameters.AddWithValue("@IdEstado", id_estado);
                cmd.Parameters.AddWithValue("@IdPedido", id);
                

                cmd2.Parameters.AddWithValue("@IdEstado", id_estado);
                cmd2.Parameters.AddWithValue("@IdPedido", id);
                cmd2.Parameters.AddWithValue("@FechaPago", fecha_pago);


                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                tran.Commit();


            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    tran.Rollback();
                throw new ApplicationException("Error al insertar cliente." + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public static List<PedidoEnvioView> obtenerPedidosPagados()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            List<PedidoEnvioView> pedidoenvios = new List<PedidoEnvioView>();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT p.nro_pedido,p.fecha_pedido,p.monto,c.nombre AS 'clientenombre',c.apellido AS 'clienteapellido',d.calle,d.numero,b.nombre AS 'barrionombre',l.nombre AS 'localidadnombre',e.nombre AS 'estadonombre' FROM pedido p INNER JOIN";
                sql += " personas c ON p.id_cliente=c.id INNER JOIN domicilios d ON d.id=c.id_domicilio INNER JOIN barrios b ON d.id_barrio=b.id INNER JOIN";
                sql += " localidades l ON l.id=b.id_localidad INNER JOIN estados e ON p.id_estado=e.id WHERE e.nombre LIKE 'Pagado' OR e.nombre LIKE 'Rechazado'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PedidoEnvioView pedidoenvio = new PedidoEnvioView();

                    pedidoenvio.NumeroPedido = (long)dr["nro_pedido"];
                    pedidoenvio.NombreCliente = dr["clientenombre"].ToString();
                    pedidoenvio.ApellidoCliente = dr["clienteapellido"].ToString();
                    pedidoenvio.Calle = dr["calle"].ToString();
                    pedidoenvio.Numero = dr["numero"].ToString();
                    pedidoenvio.Barrio = dr["barrionombre"].ToString();
                    pedidoenvio.Localidad = dr["localidadnombre"].ToString();
                    pedidoenvio.Estado = dr["estadonombre"].ToString();
                    pedidoenvio.FechaPedido = (DateTime)dr["fecha_pedido"];
                    pedidoenvio.Monto = float.Parse((dr["monto"]).ToString());

                    pedidoenvios.Add(pedidoenvio);

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

            return pedidoenvios;
        }

        public static int obtenerPedidoPorNroPedido(long nro)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            int id = 0;
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT p.id FROM pedido p INNER JOIN estados e ON p.id_estado=e.id WHERE nro_pedido=@Numero AND e.nombre LIKE 'Enviado' OR e.nombre LIKE 'Local'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Numero", nro));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    id = (int)dr["id"];
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

            return id;
        }
    }
}
