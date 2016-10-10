using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using entidades;
using negocio;

namespace creamtime
{
    public partial class AbmCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!Page.IsPostBack)
            {
                lbl_error.Visible = false;
                lbl_success.Visible = false;
                lbl_warning.Visible = false;


                //Por defecto carga Cordoba como Localidad con sus Barrios
                List<Localidad> localidades = GestorCliente.listarLocalidades();
                combo_cliente_localidad.DataSource = localidades;
                combo_cliente_localidad.DataTextField = "Nombre";
                combo_cliente_localidad.DataValueField = "Id";
                combo_cliente_localidad.DataBind();

                Localidad cba = localidades.Find(Localidad => Localidad.Nombre == "Cordoba");
                combo_cliente_barrio.DataSource = GestorCliente.listarBarrios(cba.Id);
                combo_cliente_barrio.DataTextField = "Nombre";
                combo_cliente_barrio.DataValueField = "Id";
                combo_cliente_barrio.DataBind();

                //Carga de DropDown Sexo
                combo_cliente_sexo.DataSource = GestorCliente.listarSexo();
                combo_cliente_sexo.DataTextField = "Nombre";
                combo_cliente_sexo.DataValueField = "Id";
                combo_cliente_sexo.DataBind();

               
            }
        }

        protected void combo_cliente_localidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si se carga una nueva Localidad, se cargaran de esta manera sus barrios correspondientes
            if (combo_cliente_localidad.SelectedItem.Text == "Cordoba")
            {
                int id = Convert.ToInt32(combo_cliente_localidad.SelectedValue);
                combo_cliente_barrio.DataSource = GestorCliente.listarBarrios(id);
                combo_cliente_barrio.DataTextField = "Nombre";
                combo_cliente_barrio.DataValueField = "Id";
                combo_cliente_barrio.DataBind();
            }
        }

        protected void btn_cliente_registrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    if (txt_cliente_nombre.Text != "" && txt_cliente_apellido.Text != "" && txt_cliente_dni.Text != ""
                        && txt_cliente_domicilio.Text != "" && txt_cliente_fecha_nac.Text != "" && txt_cliente_numero.Text != "" &&
                        txt_cliente_email.Text != "" && txt_cliente_telefono.Text != "" && txt_usuario.Text != "" && txt_contrasenia.Text != ""
                        && txt_contrasenia2.Text != "")
                    {
                        Int32 dni = Convert.ToInt32(txt_cliente_dni.Text);

                        if (GestorCliente.existeCliente(dni))
                        {
                            throw new ApplicationException("Cliente");
                        }

                        Cliente nuevo_cli = new Cliente();
                        Usuario nuevo_usr = new Usuario();
                        Rol rol_cliente = GestorRol.obtenerRolPorNombre("Cliente");
                        if (rol_cliente.Nombre != null)
                        {
                            nuevo_cli.Rol = rol_cliente;
                        }
                        else
                        {
                            throw new ApplicationException("Error en Rol");
                        }

                        nuevo_cli.Nombre = txt_cliente_nombre.Text;
                        nuevo_cli.Apellido = txt_cliente_apellido.Text;
                        
                        nuevo_cli.Fecha_nacimiento = Convert.ToDateTime(txt_cliente_fecha_nac.Text);
                        if ((txt_cliente_dni.Text.Length == 8 || txt_cliente_dni.Text.Length == 8) && dni >= 900000)
                        {
                            nuevo_cli.Dni = dni;
                        }
                        nuevo_cli.Email = txt_cliente_email.Text;
                        nuevo_cli.Telefono = txt_cliente_telefono.Text;
                        Sexo sexo = new Sexo();
                        sexo.Id = Convert.ToInt32(combo_cliente_sexo.SelectedItem.Value);
                        sexo.Nombre = combo_cliente_sexo.SelectedItem.Text;

                        nuevo_cli.Sexo = sexo;

                        Localidad loc = new Localidad();
                        Barrio bar = new Barrio();
                        Domicilio dom = new Domicilio();

                        loc.Id = Convert.ToInt32(combo_cliente_localidad.SelectedValue);
                        loc.Nombre = combo_cliente_localidad.SelectedItem.Text;

                        bar.Localidad = loc;
                        bar.Id = Convert.ToInt32(combo_cliente_barrio.SelectedValue);
                        bar.Nombre = combo_cliente_barrio.SelectedItem.Text;

                        dom.Barrio = bar;
                        dom.Calle = txt_cliente_domicilio.Text;
                        dom.Numero = txt_cliente_numero.Text;

                        nuevo_cli.Domicilio = dom;

                        nuevo_usr.Username = txt_usuario.Text;
                        nuevo_usr.Password = txt_contrasenia.Text;


                        if (GestorUsuario.existeUsuario(nuevo_usr))
                        {
                            throw new ApplicationException("Usuario");
                        }
                        else
                        {
                            nuevo_cli.Usuario = nuevo_usr;
                            GestorCliente.insertarCliente(nuevo_cli);
                        }

                        lbl_success.Text = "Usted ha sido registrado con exito!";
                        lbl_success.Visible = true;

                    }
                    else
                    {
                        lbl_warning.Text = "Ha dejado campos vacios en el formulario de registracion";
                        lbl_warning.Visible = true;

                    }
                }
                
                catch(ApplicationException ap)
                {
                    if (ap.Message == "Usuario")
                    {
                        lbl_warning.Text = "Atencion! El usuario ingresado ya existe, utilice otro";
                        lbl_warning.Visible = true;
                        txt_usuario.Text = "";
                        txt_contrasenia.Text = "";
                        txt_contrasenia2.Text = "";
                    }
                    if (ap.Message == "Cliente")
                    {
                        lbl_warning.Text = "Atencion! Ya existe un cliente con el DNI ingresado";
                        lbl_warning.Visible = true;
                        txt_cliente_dni.Text = "";
                        txt_contrasenia.Text = "";
                        txt_contrasenia2.Text = "";
                    }
                    else
                    {
                        lbl_error.Text = "Error en la registracion! Por favor, revise los campos e intente nuevamente";
                        lbl_error.Visible = true;
                    }

                }

                catch (Exception ex)
                {
                    lbl_error.Text = "Ha surgido un error inesperado, contacte a su administrador";
                    lbl_error.Visible=true;
                }

            }
            }


        }
    }
