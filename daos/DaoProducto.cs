﻿using entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daos
{
    public class DaoProducto
    {
        public void insertarProducto(Producto producto)
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

                cmd.CommandText = @"INSERT INTO productos (nombre
					                                      ,id_tipo
					                                      ,codigo_producto
					                                      ,precio
					                                      ,fecha_alta
					                                      ,vigente)
                                    VALUES (@Nombre
	                                       ,@IdTipo
	                                       ,@CodigoProducto
	                                       ,@Precio
	                                       ,@FechaAlta
	                                       ,@Vigente)";

                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@IdTipo", producto.Tipo_Producto.Id);
                cmd.Parameters.AddWithValue("@CodigoProducto", producto.Codigo_Producto);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@FechaAlta", producto.Fecha_Alta);
                cmd.Parameters.AddWithValue("@Vigente", producto.Vigente);

                //Inserto producto y commit a la transaccion
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    tran.Rollback();

                throw new ApplicationException("Error al insertar un nuevo producto. " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public static List<Producto> obtenerProductos(string nombre)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Entidades
            List<Producto> listaProductos = new List<Producto>();

            //Recupero los Productos
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.ConnectionString = cadenaConexion;
                cmd.Connection = con;

                cmd.CommandText = @"SELECT prod.id
	                                      ,prod.nombre
	                                      ,prod.id_tipo
	                                      ,prod.codigo_producto
	                                      ,prod.precio
	                                      ,prod.fecha_alta
	                                      ,prod.vigente
	                                      ,tpro.nombre AS 'NombreTipo'
                                      FROM productos prod INNER JOIN tipo_producto tpro
	                                    ON prod.id_tipo = tpro.id
                                     WHERE UPPER(prod.nombre) LIKE UPPER(ISNULL(@nombre, prod.nombre))";

                cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");

                //Abre conexion y consulta
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                //Agrego los productos a la lista
                while(dr.Read())
                {
                    Producto producto = new Producto();
                    TipoProducto tipo = new TipoProducto();

                    producto.Id = (int)dr["id"];
                    producto.Nombre = dr["nombre"].ToString();

                    tipo.Id = (int)dr["id_tipo"];
                    tipo.Nombre = dr["NombreTipo"].ToString();

                    producto.Tipo_Producto = tipo; 
                    producto.Codigo_Producto = (int)dr["codigo_producto"];
                    producto.Precio = float.Parse(dr["precio"].ToString());
                    producto.Fecha_Alta = (DateTime)dr["fecha_alta"];
                    producto.Vigente = (Boolean)dr["vigente"];

                    listaProductos.Add(producto);
                }

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al recuperar los productos: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return listaProductos;
        }


        public static Boolean existeProducto(int codigo)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Entidades
            Boolean existe = false;

            //Recupero los Productos
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.ConnectionString = cadenaConexion;
                cmd.Connection = con;

                cmd.CommandText = @"SELECT 1
                                      FROM productos prod 
                                     WHERE prod.codigo_producto = @codigo";

                cmd.Parameters.AddWithValue("@codigo", codigo);

                //Abre conexion y consulta
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                //Agrego los productos a la lista
                while (dr.Read())
                {
                    existe = true;
                }

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("" + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return existe;
        }


        public static Producto buscarProducto(int codigo)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Entidades
            Producto producto = new Producto();

            //Recupero los Productos
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.ConnectionString = cadenaConexion;
                cmd.Connection = con;

                cmd.CommandText = @"SELECT prod.id
	                                      ,prod.nombre
	                                      ,prod.id_tipo
	                                      ,prod.codigo_producto
	                                      ,prod.precio
	                                      ,prod.fecha_alta
	                                      ,prod.vigente
                                          ,tpro.nombre AS 'NombreTipo'
                                      FROM productos prod INNER JOIN tipo_producto tpro
	                                    ON prod.id_tipo = tpro.id
                                     WHERE prod.codigo_producto = @Codigo";

                cmd.Parameters.AddWithValue("@Codigo", codigo);

                //Abre conexion y consulta
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                //Agrego los productos a la lista
                while (dr.Read())
                {                    
                    TipoProducto tipo = new TipoProducto();

                    producto.Id = (int)dr["id"];
                    producto.Nombre = dr["nombre"].ToString();

                    tipo.Id = (int)dr["id_tipo"];
                    tipo.Nombre = dr["NombreTipo"].ToString();

                    producto.Tipo_Producto = tipo;
                    producto.Codigo_Producto = (int)dr["codigo_producto"];
                    producto.Precio = float.Parse(dr["precio"].ToString());
                    producto.Fecha_Alta = (DateTime)dr["fecha_alta"];
                    producto.Vigente = (Boolean)dr["vigente"];
                }

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al recuperar los productos: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return producto;
        }


        public static Boolean eliminarProducto(int codigo)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Entidades
            Boolean elimina = false;

            //Verifico Producto
            if (existeProducto(codigo))
            {
                //Elimino producto
                SqlCommand cmd = new SqlCommand();
                SqlTransaction tran = null;
                try
                {
                    con.ConnectionString = cadenaConexion;
                    con.Open();
                    tran = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = tran;

                    cmd.CommandText = @"DELETE FROM productos
                                        WHERE codigo_producto=@codigo";

                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    //Elimino producto y commit a la transaccion
                    cmd.ExecuteNonQuery();
                    tran.Commit();

                    elimina = true;
                }
                catch (Exception ex)
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        tran.Rollback();

                    throw new ApplicationException("Error al eliminar un producto. " + ex.Message);
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        con.Close();                    
                }
            }

            return elimina;
        }


        public static Boolean modificarProducto(Producto producto)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Entidades
            Boolean modifica = false;

            //Verifico Producto
            if (existeProducto(producto.Codigo_Producto))
            {
                //Modifico producto
                SqlCommand cmd = new SqlCommand();
                SqlTransaction tran = null;
                try
                {
                    con.ConnectionString = cadenaConexion;
                    con.Open();
                    tran = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = tran;

                    cmd.CommandText = @"UPDATE productos
                                           SET nombre = @Nombre
                                              ,id_tipo = @Tipo
                                              ,precio = @Precio
                                              ,fecha_alta = @Fecha
                                              ,vigente = @Vigente
                                         WHERE codigo_producto = @Codigo";

                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Tipo", producto.Tipo_Producto.Id);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Fecha", producto.Fecha_Alta);
                    cmd.Parameters.AddWithValue("@Vigente", producto.Vigente);
                    cmd.Parameters.AddWithValue("@Codigo", producto.Codigo_Producto);

                    //Modifica el producto y commit a la transaccion
                    cmd.ExecuteNonQuery();
                    tran.Commit();

                    modifica = true;
                }
                catch (Exception ex)
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        tran.Rollback();

                    throw new ApplicationException("Error al modificar un producto. " + ex.Message);
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        con.Close();
                }
            }

            return modifica;
        }

    }
}
