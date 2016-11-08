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
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Prerender(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                if ((String)Session["user_perm"] == "Personal")
                {
                    this.Page_Load(sender, e);
                }
                else
                {
                    Response.Redirect("~/403Forbidden", false);
                }

            }
            else
            {
                Response.Redirect("~/Login", false);
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                lbl_error.Visible = false;
                lbl_success.Visible = false;
                lbl_warning.Visible = false;

                cliente_gridview.DataSource = GestorCliente.obtenerClientes();
                string[] keys = new string[] { "Dni"};
                cliente_gridview.DataKeyNames = keys;
                cliente_gridview.DataBind();

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

        protected void boton_actualizar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    if (txt_cliente_nombre.Text != "" && txt_cliente_apellido.Text != ""
                        && txt_cliente_domicilio.Text != "" && txt_cliente_fecha_nacim.Text != "" && txt_cliente_numero.Text != "" &&
                        txt_cliente_email.Text != "" && txt_cliente_telefono.Text != "" && txt_usuario.Text != "")
                    {
        
                        Cliente nuevo_cli = new Cliente();
                        Usuario nuevo_usr = new Usuario();

                        int dni = Convert.ToInt32(Application["dni"]);
                        Cliente seleccionado = GestorCliente.obtenerClientesDni(dni);

                        nuevo_cli.Id = seleccionado.Id;

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

                        nuevo_cli.Fecha_nacimiento = Convert.ToDateTime(txt_cliente_fecha_nacim.Text);

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

                        Usuario check_user = GestorUsuario.existeUsuario(nuevo_usr);


                        nuevo_cli.Domicilio.Id = seleccionado.Domicilio.Id;
                        if(seleccionado.Domicilio.Calle == nuevo_cli.Domicilio.Calle && seleccionado.Domicilio.Numero
                           == nuevo_cli.Domicilio.Numero && seleccionado.Domicilio.Barrio.Nombre == nuevo_cli.Domicilio.Barrio.Nombre
                           && seleccionado.Domicilio.Barrio.Localidad.Nombre == nuevo_cli.Domicilio.Barrio.Localidad.Nombre)
                        {
                            nuevo_cli.Domicilio = null;
                        }

                        if (seleccionado.Usuario.Username == nuevo_usr.Username)
                        {
                            nuevo_cli.Usuario = null;
                        }

                        else
                        {

                            if (check_user.Username != null && check_user.ClienteId == null)
                            {
                                nuevo_cli.Usuario = check_user;
                                nuevo_cli.Domicilio.Id = seleccionado.Domicilio.Id;
                                nuevo_cli.Usuario.Id = seleccionado.Usuario.Id;
                            }
                            else
                            {
                                throw new ApplicationException("Usuario");
                            }
                        }


                            GestorCliente.actualizarCliente(nuevo_cli);
                            lbl_success.Text = "Cliente actualizado con exito!";
                            lbl_success.Visible = true;
                            cliente_gridview.DataSource = GestorCliente.obtenerClientes();
                            string[] keys = new string[] { "Dni" };
                            cliente_gridview.DataKeyNames = keys;
                            cliente_gridview.DataBind();

                            lbl_dni.Text = "";

                            txt_cliente_nombre.Text = "";
                            txt_cliente_apellido.Text = "";
                            txt_cliente_fecha_nacim.Text = "";
                            txt_cliente_numero.Text = "";
                            txt_cliente_email.Text = "";
                            txt_cliente_telefono.Text = "";
                            txt_cliente_domicilio.Text = "";
                            txt_usuario.Text = "";
                       


                    }
                    else
                    {
                        lbl_warning.Text = "Ha dejado campos vacios en el formulario de registracion";
                        lbl_warning.Visible = true;

                    }
                }

                catch (ApplicationException ap)
                {
                    if (ap.Message == "Usuario")
                    {
                        lbl_warning.Text = "Atencion! El usuario ingresado ya existe, utilice otro";
                        lbl_warning.Visible = true;
                        txt_usuario.Text = "";

                    }
                    if (ap.Message == "Cliente")
                    {
                        lbl_warning.Text = "Atencion! Ya existe un cliente con el DNI ingresado";
                        lbl_warning.Visible = true;

                    }
                    if (ap.Message != "Usuario" && ap.Message != "Cliente")
                    {
                        lbl_error.Text = "Error en la actualizacion! Por favor, revise los campos e intente nuevamente";
                        lbl_error.Visible = true;
                    }

                }

                catch (Exception ex)
                {
                    lbl_error.Text = "Ha surgido un error inesperado, contacte a su administrador";
                    lbl_error.Visible = true;
                }

            }
        }


        protected void cliente_gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lbl_error.Visible = false;
            lbl_success.Visible = false;
            lbl_warning.Visible = false;


            int dni = Convert.ToInt32(cliente_gridview.DataKeys[Convert.ToInt32(e.RowIndex)].Value.ToString());
            GestorCliente.eliminarCliente(dni);
            lbl_success.Text = "Cliente eliminado con exito!";
            lbl_success.Visible = true;
            cliente_gridview.DataSource = GestorCliente.obtenerClientes();
            string[] keys = new string[] { "Dni" };
            cliente_gridview.DataKeyNames = keys;
            cliente_gridview.DataBind();
        }

        protected void cliente_gridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbl_error.Visible = false;
            lbl_success.Visible = false;
            lbl_warning.Visible = false;

            int dni = Convert.ToInt32(cliente_gridview.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());
            Cliente cli = GestorCliente.obtenerClientesDni(dni);
            Application["dni"] = cli.Dni;
            lbl_dni.Text = cliente_gridview.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();

            txt_cliente_nombre.Text = cli.Nombre;
            txt_cliente_apellido.Text = cli.Apellido;
            txt_cliente_fecha_nacim.Text = Convert.ToString(cli.Fecha_nacimiento.ToShortDateString());
            combo_cliente_sexo.ClearSelection();
            combo_cliente_sexo.Items.FindByText(cli.Sexo.Nombre).Selected = true;
            combo_cliente_localidad.ClearSelection();
            combo_cliente_localidad.Items.FindByText(cli.Domicilio.Barrio.Localidad.Nombre).Selected = true;
            combo_cliente_barrio.ClearSelection();
            combo_cliente_barrio.Items.FindByText(cli.Domicilio.Barrio.Nombre).Selected = true;
            txt_cliente_numero.Text = cli.Domicilio.Numero;
            txt_cliente_email.Text = cli.Email;
            txt_cliente_telefono.Text = cli.Telefono;
            txt_cliente_domicilio.Text = cli.Domicilio.Calle;
            txt_usuario.Text = cli.Usuario.Username;
        }
    }
    }
