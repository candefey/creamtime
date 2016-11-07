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
    public class DaoMateriaPrima
    {
        public static List<MateriaPrima> listarMateriasPrimas()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            List<MateriaPrima> lista = new List<MateriaPrima>();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT mp.id AS id, mp.nombre AS nombre, mpx.precio AS precio, id_proveedor AS idProv FROM materias_primas mp INNER JOIN mp_por_proveedor mpx ON mpx.id_materia_prima=mp.id";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    MateriaPrima mp = new MateriaPrima();
                    mp.Id = (int)dr["id"];
                    mp.Nombre = dr["nombre"].ToString();
                    mp.precio = float.Parse(dr["precio"].ToString());
                    mp.IdProveedor =(int) dr["idProv"];
                    lista.Add(mp);
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

            return lista;
        }

        public static List<MateriaPrima> listarMateriasPrimas(int idp)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
          
            List<MateriaPrima> lista = new List<MateriaPrima>();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT mp.id AS id, mp.nombre AS nombre, mpx.precio AS precio, id_proveedor AS idProv FROM materias_primas mp INNER JOIN mp_por_proveedor mpx ON mpx.id_materia_prima=mp.id WHERE mpx.id_proveedor=@idFiltro";
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@idFiltro", idp));
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MateriaPrima mp = new MateriaPrima();
                    mp.Id = (int)dr["id"];
                    mp.Nombre = dr["nombre"].ToString();
                    mp.precio = float.Parse(dr["precio"].ToString());
                    mp.IdProveedor = (int)dr["idProv"];
                    lista.Add(mp);
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

            return lista;
        }



        public static MateriaPrima buscarMateriaPrima(int id)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            MateriaPrima mp = new MateriaPrima();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT mp.id AS id, mp.nombre AS nombre, mpx.precio AS precio, id_proveedor AS idProv FROM materias_primas mp INNER JOIN mp_por_proveedor mpx ON mpx.id_materia_prima=mp.id WHERE mp.id=@idFiltro";
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@idFiltro", id));
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    mp.Id = (int)dr["id"];
                    mp.Nombre = dr["nombre"].ToString();
                    mp.precio = float.Parse(dr["precio"].ToString());
                    mp.IdProveedor = (int)dr["idProv"];
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

            return mp;
        }





        public static void insertarCompras(List<DetalleCompraView> detalles, float monto)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();
            SqlTransaction tran = null;

            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();
                tran = cn.BeginTransaction();

                string sql = "INSERT INTO compras (fecha_compra, monto, nro_compra)";
                sql += " VALUES (@fecha, @monto, @nro); SELECT @@Identity;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@monto",monto);
                cmd.Parameters.AddWithValue("@nro", DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second);
                int idUltimaCompra = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var de in detalles)
                {
               
                    string sqldetalle = "INSERT INTO detalle_compra (id_compra, id_materia_prima, cantidad, precio, id_proveedor)";
                    sqldetalle += " VALUES (@idCompra, @idMP, @cant, @precio, @id_pro)";

                    SqlCommand cmddetalle = new SqlCommand();
                    cmddetalle.CommandText = sqldetalle;
                    cmddetalle.Connection = cn;
                    cmddetalle.Transaction = tran;
                    cmddetalle.Parameters.AddWithValue("@idCompra",idUltimaCompra);
                    cmddetalle.Parameters.AddWithValue("@idMP", de.IdMP);
                    cmddetalle.Parameters.AddWithValue("@cant", de.Cantidad);
                    cmddetalle.Parameters.AddWithValue("@precio", de.Monto);
                    cmddetalle.Parameters.AddWithValue("@id_pro", de.IdProveedor);

                    cmddetalle.ExecuteNonQuery();

                }

                tran.Commit();


            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    tran.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al insertar la compra");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }






    }  
}
