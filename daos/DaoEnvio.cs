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
    public static class DaoEnvio
    {
        public static List<PedidoEnvioView> obtenerEnviosPendientes()
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
                sql += " localidades l ON l.id=b.id_localidad INNER JOIN estados e ON p.id_estado=e.id WHERE e.nombre LIKE 'Delivery'";
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

        public static List<EnviosView> obtenerEnviosRealizados()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            List<EnviosView> envios = new List<EnviosView>();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT en.fecha_hora_partida,en.fecha_hora_llegada,en.nro_envio,r.nombre,r.apellido,e.nombre AS 'estadonombre' FROM envios en INNER JOIN";
                sql += " personas r ON en.id_repartidor=r.id INNER JOIN estados e ON e.id=en.id_estado WHERE e.nombre LIKE 'En camino' OR e.nombre LIKE 'Pagado'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EnviosView envio = new EnviosView();

                    envio.NombreRepartidor = dr["nombre"].ToString();
                    envio.ApellidoRepartidor = dr["apellido"].ToString();
                    envio.FechaEnvio = (DateTime)dr["fecha_hora_partida"];
                    envio.FechaLlegada = (DateTime)dr["fecha_hora_llegada"];
                    envio.NumeroEnvio = Convert.ToInt64(dr["nro_envio"]);
                    envio.Estado = dr["estadonombre"].ToString();

                    envios.Add(envio);

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

            return envios;
        }

        public static void registrarEnvio(Envio env)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();
            SqlTransaction tran = null;

            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();
                tran = cn.BeginTransaction();


                string sql = "INSERT INTO envios (id_repartidor,fecha_hora_partida,fecha_hora_llegada,id_pedido,id_estado,nro_envio)";
                sql += " VALUES (@IdRepartidor,@FechaPartida,@FechaLlegada,@IdPedido,@IdEstado,@NroEnvio);";

                Estado estado = obtenerEstadoPorNombre("Enviado");

                string sql2 = "UPDATE pedido SET id_estado=@IdEstado WHERE id=@IdPedido";

                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;

                cmd2.CommandText = sql2;
                cmd2.Connection = cn;
                cmd2.Transaction = tran;

                cmd.Parameters.AddWithValue("@IdRepartidor", env.Repartidor);
                cmd.Parameters.AddWithValue("@FechaPartida", env.Fecha_Partida);
                cmd.Parameters.AddWithValue("@FechaLlegada", env.Fecha_Llegada);
                cmd.Parameters.AddWithValue("@IdPedido", env.Pedido.ID);
                cmd.Parameters.AddWithValue("@IdEstado", env.Estado.ID);
                cmd.Parameters.AddWithValue("@NroEnvio", env.Nro_Envio);

                cmd2.Parameters.AddWithValue("@IdEstado", estado.ID);
                cmd2.Parameters.AddWithValue("@IdPedido", env.Pedido.ID);


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

        public static int obtenerPedidoPorNroPedido(long nro)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            int id = 0;
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT p.id FROM pedido p INNER JOIN estados e ON p.id_estado=e.id WHERE nro_pedido=@Numero AND e.nombre LIKE 'Delivery'";
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

        public static List<Cliente> obtenerRepartidores()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            List<Cliente> repartidores = new List<Cliente>();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT p.nombre, p.apellido, p.id FROM personas p INNER JOIN rol r ON r.id=p.id_rol WHERE r.nombre LIKE 'Repartidor'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cliente repartidor = new Cliente();

                    repartidor.Id = (int)dr["id"];
                    repartidor.Nombre = dr["nombre"].ToString() +" - "+ dr["apellido"].ToString();

                    repartidores.Add(repartidor);

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

            return repartidores;
        }

        public static Estado obtenerEstadoPorNombre(string nombre)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            Estado estado = new Estado();
            try
            {

                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT e.id,e.nombre FROM estados e WHERE nombre LIKE @Nombre";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    
                    estado.ID = (int)dr["id"];
                    estado.Nombre = dr["nombre"].ToString();
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

            return estado;
        }
    }
}
