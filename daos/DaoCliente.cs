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
              
                Domicilio domicilio = DaoDomicilio.insertarDomicilio(cli.Domicilio, cn, tran);

                string sql = "INSERT INTO personas (nombre,apellido,dni,id_rol,fecha_nacimiento,vigente,id_sexo,telefono,email,id_domicilio)";
                sql += " VALUES (@Nombre,@Apellido,@Dni,@IdRol,@FechaNacimiento,@Vigente,@IdSexo,@Telefono,@Email,@IdDomicilio);Select @@Identity;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Nombre", cli.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cli.Apellido);
                cmd.Parameters.AddWithValue("@Dni", cli.Dni);
                cmd.Parameters.AddWithValue("@IdRol", cli.Rol.Id);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cli.Fecha_nacimiento);
                cmd.Parameters.AddWithValue("@Vigente", vigente);
                cmd.Parameters.AddWithValue("@IdSexo", cli.Sexo.Id);
                cmd.Parameters.AddWithValue("@Telefono", cli.Telefono);
                cmd.Parameters.AddWithValue("@Email", cli.Email);
                cmd.Parameters.AddWithValue("@IdDomicilio", domicilio.Id);

                int cliente_id = Convert.ToInt32(cmd.ExecuteScalar());
                cli.Id = cliente_id;

                DaoUsuario.insertarUsuario(cli.Usuario, cn, tran, cli.Id);

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

        public static List<Sexo> listarSexo()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            Barrio bar = new Barrio();
            SqlConnection con = new SqlConnection();
            List<Sexo> sexos = new List<Sexo>();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM sexo";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sexo sexo = new Sexo();
                    sexo.Id = (int)dr["id"];
                    sexo.Nombre = dr["nombre"].ToString();
                    sexos.Add(sexo);
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

            return sexos;
        }

        public static Boolean existeCliente(int dni)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            Boolean flag = false;
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM personas WHERE dni=@Dni";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Dni", dni));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    flag = true;
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

            return flag;
        }


        public static Boolean esClienteVigente(int? id)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            Boolean flag = false;

            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT * FROM personas WHERE id=@Id AND vigente=1";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    flag = true;
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

            return flag;
        }

        public static string obtenerRolDeCliente(int? id)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();
            string rol = "";

            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT r.nombre AS 'nombre_rol' FROM personas p INNER JOIN rol r ON r.id=p.id_rol WHERE p.id=@Id AND vigente=1";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    rol = dr["nombre_rol"].ToString();
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

            return rol;
        }

        public static List<ClienteView> obtenerClientes()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            Barrio bar = new Barrio();
            SqlConnection con = new SqlConnection();
            List<ClienteView> clientes = new List<ClienteView>();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT p.*, u.username,d.calle,d.numero,b.nombre AS 'barrio',s.nombre AS 'sexo', l.nombre AS 'localidad' FROM personas p INNER JOIN rol r ON p.id_rol=r.id INNER JOIN usuarios u ON u.id_persona=p.id";
                sql += " INNER JOIN domicilios d ON p.id_domicilio=d.id INNER JOIN barrios b ON d.id_barrio=b.id"; 
                sql+=" INNER JOIN sexo s ON s.id=p.id_sexo INNER JOIN localidades l ON l.id=b.id_localidad WHERE p.vigente=1 and r.nombre='Cliente';";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DateTime fecha_nac = (DateTime)dr["fecha_nacimiento"];
                    var now = float.Parse(DateTime.Now.ToString("yyyyMMdd"));
                    var dob = float.Parse(fecha_nac.ToString("yyyyMMdd"));
                    var age = (int)(now - dob) / 10000;
                    ClienteView cliente = new ClienteView();
                    cliente.Nombre = dr["nombre"].ToString();
                    cliente.Apellido = dr["apellido"].ToString();
                    cliente.Dni = (long)dr["dni"];
                    cliente.Usuario = dr["username"].ToString();
                    cliente.Edad = age;
                    cliente.Sexo = dr["sexo"].ToString();
                    cliente.Telefono = dr["telefono"].ToString();
                    cliente.Email = dr["email"].ToString();
                    cliente.Calle = dr["calle"].ToString();
                    cliente.Numero = dr["numero"].ToString();
                    cliente.Barrio = dr["barrio"].ToString();
                    cliente.Localidad = dr["localidad"].ToString();

                    clientes.Add(cliente);

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

            return clientes;
        }
    }
}
