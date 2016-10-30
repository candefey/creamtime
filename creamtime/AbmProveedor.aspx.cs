using entidades;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace creamtime
{
    public partial class AbmProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cargarGrilla();
            if (!Page.IsPostBack)
            {
                lbl_error.Visible = false;
                lbl_success.Visible = false;
                lbl_warning.Visible = false;


                //Por defecto carga Cordoba como Localidad con sus Barrios
                List<Localidad> localidades = GestorProveedor.listarLocalidades();
                combo_proveedor_localidad.DataSource = localidades;
                combo_proveedor_localidad.DataTextField = "Nombre";
                combo_proveedor_localidad.DataValueField = "Id";
                combo_proveedor_localidad.DataBind();

                Localidad cba = localidades.Find(Localidad => Localidad.Nombre == "Cordoba");
                combo_proveedor_barrio.DataSource = GestorProveedor.listarBarrios(cba.Id);
                combo_proveedor_barrio.DataTextField = "Nombre";
                combo_proveedor_barrio.DataValueField = "Id";
                combo_proveedor_barrio.DataBind();

                cargarGrilla();
                
            }
        }
        protected void combo_proveedor_localidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si se carga una nueva Localidad, se cargaran de esta manera sus barrios correspondientes
            if (combo_proveedor_localidad.SelectedItem.Text == "Cordoba")
            {
                int id = Convert.ToInt32(combo_proveedor_localidad.SelectedValue);
                combo_proveedor_barrio.DataSource = GestorProveedor.listarBarrios(id);
                combo_proveedor_barrio.DataTextField = "Nombre";
                combo_proveedor_barrio.DataValueField = "Id";
                combo_proveedor_barrio.DataBind();
            }
        }

        protected void btn_proveedor_registrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    if (txt_razon.Text != "" && txt_cuit.Text != "" && txt_proveedor_domicilio.Text != "" &&
                        txt_proveedor_email.Text != "" && txt_proveedor_telefono.Text != "")
                    {

                        Proveedor nuevo_pro = new Proveedor();
                        nuevo_pro.RazonSocial = txt_razon.Text;
                        Int64 cuit = Convert.ToInt64(txt_cuit.Text);
                        if ((txt_cuit.Text.Length == 10 || txt_cuit.Text.Length == 11))
                        {
                            if (GestorProveedor.existeProveedor(cuit))
                                throw new ApplicationException("Proveedor");
                            else
                                nuevo_pro.Cuit = cuit;
                        }
                        else
                        {
                            throw new ApplicationException("CuitErroneo");
                        }
                        nuevo_pro.Email = txt_proveedor_email.Text;
                        nuevo_pro.Telefono = txt_proveedor_telefono.Text;
                        Sexo sexo = new Sexo();
                        Localidad loc = new Localidad();
                        Barrio bar = new Barrio();
                        Domicilio dom = new Domicilio();

                        loc.Id = Convert.ToInt32(combo_proveedor_localidad.SelectedValue);
                        loc.Nombre = combo_proveedor_localidad.SelectedItem.Text;

                        bar.Localidad = loc;
                        bar.Id = Convert.ToInt32(combo_proveedor_barrio.SelectedValue);
                        bar.Nombre = combo_proveedor_barrio.SelectedItem.Text;

                        dom.Barrio = bar;
                        dom.Calle = txt_proveedor_domicilio.Text;
                        dom.Numero = txt_proveedor_numero.Text;

                        nuevo_pro.Domicilio = dom;
                        nuevo_pro.FechaDeAlta = DateTime.Now;
                        Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
                        lbl_success.Text = "Proveedor registrado con éxito!";
                        lbl_success.Visible = true;
                        lbl_error.Visible = false;
                        lbl_warning.Visible = false;
                        GestorProveedor.insertarProveedor(nuevo_pro);
                        this.cargarGrilla();

                    }
                    else
                    {
                        lbl_warning.Text = "Ha dejado campos vacíos en el formulario de registro";
                        lbl_warning.Visible = true;

                    }
                }
                catch (ApplicationException ap)
                {
                    if (ap.Message == "Proveedor")
                    {
                        lbl_warning.Text = "Atención! Ya existe un proveedor con el CUIT ingresado";
                        lbl_warning.Visible = true;
                        lbl_error.Visible = false;
                        lbl_success.Visible = false;
                        txt_cuit.Text = "";
                        txt_proveedor_domicilio.Text = "";
                        txt_proveedor_numero.Text = "";
                        txt_proveedor_telefono.Text = "";
                        txt_proveedor_email.Text = "";
                        txt_razon.Text = "";
                    }
                    if (ap.Message == "CuitErroneo")
                    {
                        lbl_warning.Text = "Atención! El CUIT ingresado no es correcto";
                        lbl_warning.Visible = true;
                        lbl_error.Visible = false;
                        lbl_success.Visible = false;
                        txt_cuit.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    lbl_error.Text = "Ha surgido un error en la creación del proveedor" + ex;
                    lbl_error.Visible = true;
                    lbl_success.Visible = false;
                    lbl_warning.Visible = false;
                    txt_cuit.Text = "";
                    txt_proveedor_domicilio.Text = "";
                    txt_proveedor_numero.Text = "";
                    txt_proveedor_telefono.Text = "";
                    txt_proveedor_email.Text = "";
                    txt_razon.Text = "";

                }
                

                }
        }

        protected void cargarGrilla()
        {

            grillaProveedores.DataSource = GestorProveedor.listarProveedores();
            string[] keys = new string[] {"Cuit"};
            grillaProveedores.DataKeyNames = keys;
            grillaProveedores.DataBind();

        }
        

        protected void grillaProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "actualizar")
            {

                //ACTUALIZO
            }
            if (e.CommandName == "eliminar")
            {
                //ELIMINO

                //RECUPERO DATA KEY
                String cuit= grillaProveedores.DataKeys[0].Value.ToString();
                Int64 cuitint = Convert.ToInt64(cuit);
                GestorProveedor.eliminarProveedor(cuitint);
            }
        }

        protected void grillaProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            String cuit = grillaProveedores.DataKeys[0].Value.ToString();
            Int64 cuitint = Convert.ToInt64(cuit);
            GestorProveedor.eliminarProveedor(cuitint);
        }

        protected void grillaProveedores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String cuit = grillaProveedores.DataKeys[0].Value.ToString();
            Int64 cuitint = Convert.ToInt64(cuit);
            GestorProveedor.eliminarProveedor(cuitint);
            cargarGrilla();
        }

        protected void grillaProveedores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            String cuit = grillaProveedores.DataKeys[0].Value.ToString();
            Int64 cuitint = Convert.ToInt64(cuit);
            Proveedor p = GestorProveedor.buscarProveedor(cuitint);

            txt_cuit.Text = "" + p.Cuit;
            txt_cuit.Enabled = false;
            txt_proveedor_domicilio.Text = "" + p.Domicilio.Calle;
            txt_proveedor_numero.Text = "" + p.Domicilio.Numero;
            txt_proveedor_telefono.Text = "" + p.Telefono;
            txt_proveedor_email.Text = "" + p.Email;
            txt_razon.Text = "" + p.RazonSocial;
            string barrio = p.Domicilio.Barrio.Nombre;
            combo_proveedor_barrio.SelectedItem.Text = barrio;
            string localidad= p.Domicilio.Barrio.Localidad.Nombre;
            combo_proveedor_localidad.SelectedItem.Text = localidad;
        }

        protected void btn_proveedor_actualizar_Click(object sender, EventArgs e)
        {

        }
    }
}