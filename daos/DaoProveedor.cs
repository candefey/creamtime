using entidades;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace daos
{
    public class DaoProveedor
    {
        public void insertarProveedor(Proveedor p)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();
            SqlTransaction tran = null;

            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();
                tran = cn.BeginTransaction();
                int vigente = 1; //Por defecto inserta activo
                Domicilio domicilio = DaoDomicilio.insertarDomicilio(p.Domicilio,cn,tran);

                string sql = "INSERT INTO proveedores (razon_social,cuit,vigente,fecha_alta,id_domicilio, telefono,email)";
                sql += " VALUES (@RazonSocial,@Cuit,@Vigente,@FechaAlta,@IdDomicilio, @Telefono, @Email)";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@RazonSocial", p.RazonSocial);
                cmd.Parameters.AddWithValue("@Cuit", p.Cuit);
                cmd.Parameters.AddWithValue("@Vigente", vigente);
                cmd.Parameters.AddWithValue("@FechaAlta", p.FechaDeAlta);
                cmd.Parameters.AddWithValue("@IdDomicilio", domicilio.Id);
                cmd.Parameters.AddWithValue("@Telefono", p.Telefono);
                cmd.Parameters.AddWithValue("@Email", p.Email);
                cmd.ExecuteNonQuery();

                tran.Commit();


            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    tran.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al insertar el proveedor." + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }
    }
}
