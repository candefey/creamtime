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
    public partial class InformeCompras : System.Web.UI.Page
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

                lbl_error_.Visible = false;
                lbl_success_.Visible = false;
                lbl_warning_.Visible = false;

                combo_proveedores.Enabled = false;
                combo_mp.Enabled = false;
                txt_desde.Enabled = false;
                txt_hasta.Enabled = false;
                btn_filtrar.Enabled = false;

                //Combo Proveedor
                List<ProveedorView> proveedores = GestorProveedor.listarProveedores();
                combo_proveedores.DataSource = proveedores;
                combo_proveedores.DataTextField = "RazonSocial";
                combo_proveedores.DataValueField = "Id";
                combo_proveedores.DataBind();

                combo_proveedores.ClearSelection();
                combo_proveedores.Items.Add("Sin selección");
                combo_proveedores.Items.FindByText("Sin selección").Selected = true;

                //Combo Materia Prima (EN EL INFORME NO DEPENDE DEL PROVEEDOR - Hay materias primas que son de varios proveedores.
                //No discrimina por proveedor
                combo_mp.DataSource = GestorNuevaCompra.listarMP();
                combo_mp.DataTextField = "Nombre";
                combo_mp.DataValueField = "Id";
                combo_mp.DataBind();

                combo_mp.ClearSelection();
                combo_mp.Items.Add("Sin selección");
                combo_mp.Items.FindByText("Sin selección").Selected = true;
                grillaCompras.DataSource = null;
                grillaDetalleCompras.DataSource = null;
            }
        }

        protected void btn_filtrar_Click(object sender, EventArgs e)
        {
            
            try
            {
               
                if (combo_proveedores.Items.FindByText("Sin selección").Selected==false)
                {
                    int idp = Convert.ToInt16(combo_proveedores.SelectedValue);
                    //int idmp = Convert.ToInt16(combo_mp.SelectedValue);
                    List<Compra> lista_compras = GestorInformeCompras.listarCompraPorProveedor(idp);
                    grillaCompras.DataSource = null;
                    grillaCompras.DataSource = lista_compras;
                    string[] keys = new string[] { "Id" };
                    grillaCompras.DataKeyNames = keys;
                    grillaCompras.DataBind();
                }
                if (combo_mp.Items.FindByText("Sin selección").Selected==false)
                {
                    int idmp = Convert.ToInt16(combo_mp.SelectedValue);
                    List<Compra> lista_compras = GestorInformeCompras.listarCompraPorMateria(idmp);
                    grillaCompras.DataSource = null;
                    grillaCompras.DataSource = lista_compras;
                    string[] keys = new string[] { "Id" };
                    grillaCompras.DataKeyNames = keys;
                    grillaCompras.DataBind();
                }
                if (txt_desde.Text != "" && txt_hasta.Text != "")
                {
                    float desde = float.Parse(txt_desde.Text);
                    float hasta = float.Parse(txt_hasta.Text);
                    List<Compra> lista_compras = GestorInformeCompras.listarCompraMonto(desde, hasta);
                    grillaCompras.DataSource = null;
                    grillaCompras.DataSource = lista_compras;
                    string[] keys = new string[] { "Id" };
                    grillaCompras.DataKeyNames = keys;
                    grillaCompras.DataBind();
                }
                grillaDetalleCompras.DataSource = null;
                grillaDetalleCompras.DataBind();
            }
            catch (ApplicationException ex)
            {
                lbl_error_.Text = "Ha surgido un error inesperado, contacte a su administrador";
                lbl_error_.Visible = true;
            }
        }

        protected void grillaCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            String id = grillaCompras.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Int32 id_compra = Convert.ToInt32(id);
            List<DetalleCompra> lista_detalles = GestorInformeCompras.listarDetalleCompra(id_compra);

            List<DetalleCompraView> lista_detalles_grilla = new List<DetalleCompraView>();

            grillaDetalleCompras.DataSource = null;
            grillaDetalleCompras.DataSource = lista_detalles;
            grillaDetalleCompras.DataBind();
        }


        
        protected void combo_filtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_filtros.Items.FindByText("Sin Filtro").Selected == true)
            {
                grillaDetalleCompras.DataSource = null;
                grillaDetalleCompras.DataBind();
                grillaCompras.DataSource = null;
                grillaCompras.DataBind();
                combo_proveedores.Enabled = false;
                combo_mp.Enabled = false;
                txt_desde.Enabled = false;
                txt_hasta.Enabled = false;
                btn_filtrar.Enabled = false;
            }
            if (combo_filtros.Items.FindByText("Proveedor").Selected==true)
            {
                combo_proveedores.Enabled = true;
                combo_mp.Enabled = false;
                txt_desde.Enabled = false;
                txt_hasta.Enabled = false;
                btn_filtrar.Enabled = true;
            }
            if (combo_filtros.Items.FindByText("Materia Prima").Selected == true)
            {
                combo_proveedores.Enabled = false;
                combo_mp.Enabled = true;
                txt_desde.Enabled = false;
                txt_hasta.Enabled = false;
                btn_filtrar.Enabled = true;
            }
            if (combo_filtros.Items.FindByText("Rango Monto").Selected == true)
            {
                combo_proveedores.Enabled = false;
                combo_mp.Enabled = false;
                txt_desde.Enabled = true;
                txt_hasta.Enabled = true;
                btn_filtrar.Enabled = true;
            }
        }
    }
}