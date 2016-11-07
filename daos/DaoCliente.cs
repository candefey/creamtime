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
    public static class DaoCliente
    {
        public static void insertarCliente(Cliente cli)
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

        public static void actualizarCliente(Cliente cli)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();
            SqlTransaction tran = null;

            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();
                tran = cn.BeginTransaction();

                if(cli.Domicilio!=null)
                {
                    DaoDomicilio.actualizarDomicilio(cli.Domicilio, cn, tran);
                }
                if(cli.Usuario!=null)
                {
                    DaoUsuario.actualizarUsuario(cli.Usuario, cn, tran, cli.Id);
                }

                string sql = "UPDATE personas SET ";
                sql+= "nombre=@Nombre,apellido=@Apellido,fecha_nacimiento=@FechaNacimiento,id_sexo=@IdSexo,telefono=@Telefono,email=@Email WHERE id=@Id";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Nombre", cli.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cli.Apellido);
                cmd.Parameters.AddWithValue("@Id", cli.Id);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cli.Fecha_nacimiento);
                cmd.Parameters.AddWithValue("@IdSexo", cli.Sexo.Id);
                cmd.Parameters.AddWithValue("@Telefono", cli.Telefono);
                cmd.Parameters.AddWithValue("@Email", cli.Email);

                cmd.ExecuteNonQuery();

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

        public static void eliminarCliente(int dni)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection cn = new SqlConnection();


            try
            {
                cn.ConnectionString = cadenaConexion;
                cn.Open();


                string sql = "UPDATE personas SET ";
                sql += "vigente=0 WHERE dni=@Dni";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@Dni", dni);
                cmd.ExecuteNonQuery();
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

        public static Cliente obtenerClienteDni(int dni)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            Barrio bar = new Barrio();
            Localidad loc = new Localidad();
            Usuario user = new Usuario();
            Cliente cli = new Cliente();
            Sexo sexo = new Sexo();
            Domicilio dom = new Domicilio();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = cadenaConexion;
                con.Open();
                string sql = "SELECT p.*, u.username,u.id AS 'userid',d.calle,d.numero,d.id AS 'domicilioid',b.nombre AS 'barrio',s.nombre AS 'sexo', l.nombre AS 'localidad' FROM personas p INNER JOIN rol r ON p.id_rol=r.id INNER JOIN usuarios u ON u.id_persona=p.id";
                sql += " INNER JOIN domicilios d ON p.id_domicilio=d.id INNER JOIN barrios b ON d.id_barrio=b.id";
                sql += " INNER JOIN sexo s ON s.id=p.id_sexo INNER JOIN localidades l ON l.id=b.id_localidad WHERE p.vigente=1 and r.nombre='Cliente' and p.dni=@Dni;";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Dni", dni);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cli.Id = (int)dr["id"];
                    cli.Fecha_nacimiento = (DateTime)dr["fecha_nacimiento"];
                    cli.Nombre = dr["nombre"].ToString();
                    cli.Apellido = dr["apellido"].ToString();
                    cli.Dni = (long)dr["dni"];
                    user.Username = dr["username"].ToString();
                    user.Id = (int)dr["userid"];
                    sexo.Nombre = dr["sexo"].ToString();
                    cli.Telefono = dr["telefono"].ToString();
                    cli.Email = dr["email"].ToString();
                    dom.Calle = dr["calle"].ToString();
                    dom.Numero = dr["numero"].ToString();
                    dom.Id = (int)dr["domicilioid"];
                    bar.Nombre = dr["barrio"].ToString();
                    loc.Nombre = dr["localidad"].ToString();
                    bar.Localidad = loc;
                    dom.Barrio = bar;
                    cli.Domicilio = dom;
                    cli.Sexo = sexo;
                    cli.Usuario = user;        

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

            return cli;
        }


        public static Cliente obtenerClienteUsuario(string username)
        {
            //Conexion
            string cadenaConexion = ConfigurationManager.ConnectionStrings["CreamTimeConexion"].ConnectionString;
            SqlConnection con = new SqlConnection();

            //Entidades
            Cliente cliente = new Cliente();

            //Recupero los Productos
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.ConnectionString = cadenaConexion;
                cmd.Connection = con;

                cmd.CommandText = @"SELECT cli.id
                                          ,cli.nombre
	                                      ,cli.apellido
                                      FROM personas cli INNER JOIN usuarios usu
                                        ON usu.id_persona = cli.id
                                     WHERE usu.username = @username";

                cmd.Parameters.AddWithValue("@username", username);

                //Abre conexion y consulta
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                //Agrego los productos a la lista
                while (dr.Read())
                {
                    cliente.Id = (int)dr["id"];
                    cliente.Nombre = dr["nombre"].ToString();
                    cliente.Apellido = dr["apellido"].ToString();
                }

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al recuperar el cliente: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return cliente;
        }
    }
}
