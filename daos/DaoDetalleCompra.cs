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
    public class DaoDetalleCompra
    {
        public static List<DetalleCompra> listarDetalleCompra(int idCompra)
        {

            List<DetalleCompra> detalle_compras = new List<DetalleCompra>();
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;


            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string consulta = "SELECT dc.id AS id, dc.id_compra AS id_com, dc.id_materia_prima AS materia, dc.cantidad AS cant, dc.precio AS pre, dc.id_proveedor AS idp, p.razon_social AS nom_p, mp.nombre AS nom_mp FROM detalle_compra dc INNER JOIN materias_primas mp ON mp.id = dc.id_materia_prima INNER JOIN proveedores p ON p.id=dc.id_proveedor WHERE id_compra=@idFiltro";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = consulta;
                cmd.Parameters.Add(new SqlParameter("@idFiltro", idCompra));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DetalleCompra dc = new DetalleCompra();
                    dc.Id = (int)reader["id"];
                    dc.IdCompra = (int)reader["id_com"];
                    dc.IdMP = (int)reader["materia"];
                    dc.Cantidad = (int)reader["cant"];
                    dc.Monto = float.Parse((reader["pre"]).ToString());
                    dc.IdProveedor= (int)reader["idp"];
                    dc.NombreMp=(string)reader["nom_mp"];
                    dc.NombreProveedor = (string)reader["nom_p"];
                    detalle_compras.Add(dc);
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

            return detalle_compras;

        }
    }
}
