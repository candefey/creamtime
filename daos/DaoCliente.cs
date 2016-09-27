﻿using System;
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
    public class DaoCliente
    {
        public void insertarCliente(Cliente cli)
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
                Usuario usuario = DaoUsuario.insertarUsuario(cli.Usuario);
                Domicilio domicilio = DaoDomicilio.insertarDomicilio(cli.Domicilio);

                string sql = "INSERT INTO personas (nombre,apellido,dni,id_rol,id_usuario,fecha_nacimiento,vigente,id_sexo,telefono,email,id_domicilio)";
                sql += " VALUES (@Nombre,@Apellido,@Dni,@IdRol,@IdUsuario,@FechaNacimiento,@Vigente,@IdSexo,@Telefono,@Email,@IdDomicilio)";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Nombre", cli.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cli.Nombre);
                cmd.Parameters.AddWithValue("@Dni", cli.Dni);
                cmd.Parameters.AddWithValue("@IdRol", cli.Rol.Id);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.Id);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cli.Fecha_nacimiento);
                cmd.Parameters.AddWithValue("@Vigente", vigente);
                cmd.Parameters.AddWithValue("@IdSexo", cli.Sexo.Id);
                cmd.Parameters.AddWithValue("@Telefono", cli.Telefono);
                cmd.Parameters.AddWithValue("@Email", cli.Email);
                cmd.Parameters.AddWithValue("@IdDomicilio", domicilio.Id);

                cmd.ExecuteNonQuery();

                tran.Commit();


            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    tran.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al insertar cliente." + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }
    }
}
