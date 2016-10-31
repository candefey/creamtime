using entidades;
using System;
using System.Collections.Generic;
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

        public static List<Proveedor> listarProveedores() {

            List<Proveedor> proveedores= new List<Proveedor>();
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
          

            SqlConnection con = new SqlConnection();
            try {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string consulta = "SELECT P.id AS id, P.razon_social AS rs, P.cuit AS cuit, P.vigente AS v, P.fecha_alta AS fa, P.telefono AS tel, P.email AS mail, D.id AS iddom, D.calle AS calledom, D.numero AS numdom, B.id AS idbarrio, B.nombre AS nombrebarrio, L.id AS idloc, L.nombre AS nomloc FROM proveedores P INNER JOIN domicilios D ON P.id_domicilio=D.id INNER JOIN barrios B ON B.id = D.id_barrio INNER JOIN localidades L ON L.id = B.id_localidad";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = consulta;                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Proveedor p = new Proveedor();
                    Domicilio dom = new Domicilio();
                    Barrio barrio = new Barrio();
                    Localidad localidad = new Localidad();
                    localidad.Id = (int)reader["idloc"];
                    localidad.Nombre = (string)reader["nomloc"];

                    barrio.Id= (int)reader["idbarrio"];
                    barrio.Nombre=(string)reader["nombrebarrio"];
                    barrio.Localidad = localidad;

                    dom.Id = (int)reader["iddom"];
                    dom.Calle = (string)reader["calledom"];
                    dom.Numero = reader["numdom"].ToString();
                    dom.Barrio = barrio;

                    p.Id = (int)reader["id"];
                    p.RazonSocial =(string)reader["rs"];
                    p.Cuit = (long)reader["cuit"];
                    p.Vigente = (int) reader["v"]; //Problema por ser tinyint en la base de datos.
                    p.FechaDeAlta = (DateTime)reader["fa"];
                    p.Telefono=(string) reader["tel"];
                    p.Email = (string)reader["mail"];
                    p.Domicilio = dom;

                    proveedores.Add(p);
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

            return proveedores;

        }
        public static Boolean existeProveedor(long cuit)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            Boolean flag = false;
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM proveedores WHERE cuit=@cuit";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@cuit", cuit));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    flag = true;
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

            return flag;
        }

        public static Boolean eliminarProveedor(long cuit)
        {
            Boolean b = false;
            if (existeProveedor(cuit)){ 
                string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
                SqlConnection con = new SqlConnection();
                try
                {
                    con.ConnectionString = cadenaConexion;
                    con.Open();
                    string sql = "DELETE FROM proveedores WHERE cuit=@cuit";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@cuit", cuit));
                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    throw new ApplicationException("" + ex.Message);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    b = true;
                }
            }

            return b;

        }

        public static Boolean actualizarProveedor(Proveedor p)
        {
            Boolean b = false;
            if (existeProveedor(p.Cuit))
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
                SqlConnection con = new SqlConnection();
                SqlTransaction tran = null;
                try
                {
                    con.ConnectionString = cadenaConexion;
                    con.Open();
                    tran = con.BeginTransaction();
                   
                    string sql = "UPDATE proveedores SET proveedores.razon_social=@RS, proveedores.vigente=@vigente, proveedores.telefono=@tel, proveedores.email=@email WHERE cuit=@cuit";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.Transaction = tran;                   
                    cmd.Parameters.Add(new SqlParameter("@RS", p.RazonSocial));
                    cmd.Parameters.Add(new SqlParameter("@vigente", p.Vigente));
                    cmd.Parameters.Add(new SqlParameter("@tel", p.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@email", p.Email));
                    cmd.Parameters.Add(new SqlParameter("@cuit", p.Cuit));
                    cmd.ExecuteNonQuery();


                    string sql2 = "UPDATE domicilios SET domicilios.calle=@calle, domicilios.numero=@num, domicilios.id_barrio=@idB WHERE domicilios.id=@idDom";
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = sql2;
                    cmd.Connection = con;
                    cmd.Transaction = tran;
                    cmd2.Parameters.Add(new SqlParameter("@calle", p.Domicilio.Calle));
                    cmd2.Parameters.Add(new SqlParameter("@num", p.Domicilio.Numero));
                    cmd2.Parameters.Add(new SqlParameter("@idB", p.Domicilio.Barrio.Id));
                    cmd2.Parameters.Add(new SqlParameter("@idDom", p.Domicilio.Id));
                    cmd.ExecuteNonQuery();

                    tran.Commit();
                    

                }
                catch (SqlException ex)
                {
                    throw new ApplicationException("" + ex.Message);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    b = true;
                }
            }

            return b;

        }

        public static Proveedor buscarProveedor(long cuit)
        {

            
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            Proveedor p = new Proveedor();

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string consulta = "SELECT P.id AS id, P.razon_social AS rs, P.cuit AS cuit, P.vigente AS v, P.fecha_alta AS fa, P.telefono AS tel, P.email AS mail, D.id AS iddom, D.calle AS calledom, D.numero AS numdom, B.id AS idbarrio, B.nombre AS nombrebarrio, L.id AS idloc, L.nombre AS nomloc FROM proveedores P INNER JOIN domicilios D ON P.id_domicilio=D.id INNER JOIN barrios B ON B.id = D.id_barrio INNER JOIN localidades L ON L.id = B.id_localidad WHERE P.cuit=@cuit";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = consulta;
                cmd.Parameters.Add(new SqlParameter("@cuit", cuit));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    Domicilio dom = new Domicilio();
                    Barrio barrio = new Barrio();
                    Localidad localidad = new Localidad();
                    localidad.Id = (int)reader["idloc"];
                    localidad.Nombre = (string)reader["nomloc"];

                    barrio.Id = (int)reader["idbarrio"];
                    barrio.Nombre = (string)reader["nombrebarrio"];
                    barrio.Localidad = localidad;

                    dom.Id = (int)reader["iddom"];
                    dom.Calle = (string)reader["calledom"];
                    dom.Numero = reader["numdom"].ToString();
                    dom.Barrio = barrio;

                    p.Id = (int)reader["id"];
                    p.RazonSocial = (string)reader["rs"];
                    p.Cuit = (long)reader["cuit"];
                    p.Vigente = (int)reader["v"]; //Problema por ser tinyint en la base de datos.
                    p.FechaDeAlta = (DateTime)reader["fa"];
                    p.Telefono = (string)reader["tel"];
                    p.Email = (string)reader["mail"];
                    p.Domicilio = dom;
                    
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

            return p;

        }

    }

}
