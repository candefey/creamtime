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
            combo_proveedor_localidad.ClearSelection();
            combo_proveedor_barrio.ClearSelection();
            if (!Page.IsPostBack)
            {
                lbl_error.Visible = false;
                lbl_success.Visible = false;
                lbl_warning.Visible = false;
                lbl_fecha_de_modif.Visible = false;
                
                lbl_titulo_fecha_alta.Visible = false;
                lbl_fecha_de_alta.Visible = false;
                ti_new.Visible = true;
                ti_update.Visible = false;
                check_vigente.Checked = false;
                btn_proveedor_actualizar.Visible = false;
                //Por defecto carga Cordoba como Localidad con sus Barrios
                List<Localidad> localidades = GestorProveedor.listarLocalidades();
                combo_proveedor_localidad.DataSource = localidades;
                combo_proveedor_localidad.DataTextField = "Nombre";
                combo_proveedor_localidad.DataValueField = "Id";                
                combo_proveedor_localidad.DataBind();
                combo_proveedor_localidad.Items.Add("Sin selección");
                combo_proveedor_localidad.Items.FindByText("Sin selección").Selected = true;

                Localidad cba = localidades.Find(Localidad => Localidad.Nombre == "Cordoba");
                combo_proveedor_barrio.DataSource = GestorProveedor.listarBarrios(cba.Id);
                combo_proveedor_barrio.DataTextField = "Nombre";
                combo_proveedor_barrio.DataValueField = "Id";
                combo_proveedor_barrio.DataBind();
                combo_proveedor_barrio.Items.Add("Sin selección");
                combo_proveedor_barrio.Items.FindByText("Sin selección").Selected = true;

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
                        if (check_vigente.Checked)
                            nuevo_pro.Vigente = 1;
                        else
                            nuevo_pro.Vigente = 0;
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
            string[] keys = new string[] { "Cuit" };
            grillaProveedores.DataKeyNames = keys;
            grillaProveedores.DataBind();

        }


        protected void grillaProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
                ti_update.Visible = true;
                ti_new.Visible = false;
                btn_proveedor_actualizar.Visible = true;
                btn_proveedor_registrar.Visible = false;
                int index= e.CommandArgument.GetHashCode();
                String cuit = grillaProveedores.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                Int64 cuitint = Convert.ToInt64(cuit);
                Proveedor p = GestorProveedor.buscarProveedor(cuitint);
                txt_cuit.Text = "" + p.Cuit;
                txt_cuit.Enabled = false;
                txt_proveedor_domicilio.Text = "" + p.Domicilio.Calle;
                txt_proveedor_numero.Text = "" + p.Domicilio.Numero;
                txt_proveedor_telefono.Text = "" + p.Telefono;
                txt_proveedor_email.Text = "" + p.Email;
                txt_razon.Text = "" + p.RazonSocial;
            
            if (p.Vigente == 0)
                check_vigente.Checked = false;
            else
                check_vigente.Checked = true;
            combo_proveedor_localidad.ClearSelection();
            combo_proveedor_barrio.ClearSelection();
            combo_proveedor_barrio.Items.FindByText(p.Domicilio.Barrio.Nombre).Selected = true;
            combo_proveedor_localidad.Items.FindByText(p.Domicilio.Barrio.Localidad.Nombre).Selected = true;
            lbl_fecha_de_alta.Visible = true;   
            lbl_fecha_de_alta.Text = Convert.ToString(p.FechaDeAlta.ToShortDateString());
            lbl_titulo_fecha_alta.Visible = true;
            
            
        }

      
        protected void grillaProveedores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String cuit = grillaProveedores.DataKeys[Convert.ToInt32(e.RowIndex)].Value.ToString();
            Int64 cuitint = Convert.ToInt64(cuit);
            GestorProveedor.eliminarProveedor(cuitint);
            cargarGrilla();
        }

    
        protected void btn_proveedor_actualizar_Click(object sender, EventArgs e)
        {
            ti_new.Visible = true;
            ti_update.Visible = false;
            btn_proveedor_registrar.Visible = true;
            btn_proveedor_actualizar.Visible = false;


            if (Page.IsValid)
            {
                try
                {

                    if (txt_razon.Text != "" && txt_cuit.Text != "" && txt_proveedor_domicilio.Text != "" &&
                        txt_proveedor_email.Text != "" && txt_proveedor_telefono.Text != "")
                    {

                        Proveedor nuevo_pro = new Proveedor();
                        nuevo_pro.RazonSocial = txt_razon.Text;
                        if (check_vigente.Checked)
                            nuevo_pro.Vigente = 1;
                        else
                            nuevo_pro.Vigente = 0;
                        Int64 cuit = Convert.ToInt64(txt_cuit.Text);
                        nuevo_pro.Cuit = cuit;
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
                        GestorProveedor.actualizarProveedor(nuevo_pro);
                        lbl_success.Text = "Proveedor actualizado con éxito!";
                        lbl_success.Visible = true;
                        lbl_error.Visible = false;
                        lbl_warning.Visible = false;
                        lbl_fecha_de_modif.Visible = false;
                        txt_cuit.Text = "";
                        txt_proveedor_domicilio.Text = "";
                        txt_proveedor_numero.Text = "";
                        txt_proveedor_telefono.Text = "";
                        txt_proveedor_email.Text = "";
                        txt_razon.Text = "";
                        combo_proveedor_localidad.ClearSelection();
                        combo_proveedor_barrio.ClearSelection();
                        combo_proveedor_localidad.Items.FindByText("Sin selección").Selected = true;
                        combo_proveedor_barrio.Items.FindByText("Sin selección").Selected = true;                        
                        this.cargarGrilla();

                    }
                    else
                    {
                        lbl_warning.Text = "Ha dejado campos vacíos en el formulario de modificación";
                        lbl_warning.Visible = true;

                    }
                } 
                
                catch (Exception ex)
                {
                    lbl_error.Text = "Ha surgido un error en la modificación del proveedor" + ex;
                    lbl_error.Visible = true;
                    lbl_success.Visible = false;
                    lbl_warning.Visible = false;
                    txt_cuit.Text = "";
                    txt_proveedor_domicilio.Text = "";
                    txt_proveedor_numero.Text = "";
                    txt_proveedor_telefono.Text = "";
                    txt_proveedor_email.Text = "";
                    txt_razon.Text = "";

                    combo_proveedor_localidad.ClearSelection();
                    combo_proveedor_barrio.ClearSelection();
                    combo_proveedor_localidad.Items.FindByText("Sin selección").Selected = true;
                    combo_proveedor_barrio.Items.FindByText("Sin selección").Selected = true;

                }

            }
        }
    }
}