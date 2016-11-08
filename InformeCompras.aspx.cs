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

                 List<Compra> lista_compras = GestorInformeCompras.listarCompras(null, null, null, null);
                    grillaCompras.DataSource = null;
                    grillaCompras.DataSource = lista_compras;
                    string[] keys = new string[] { "Id" };
                    grillaCompras.DataKeyNames = keys;
                    grillaCompras.DataBind();
            }
        }

        protected void btn_filtrar_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (combo_proveedores.Items.FindByText("Sin selección").Selected==false || combo_mp.Items.FindByText("Sin selección").Selected == false || txt_desde.Text != "" && txt_hasta.Text != "" && Convert.ToInt32(txt_desde.Text) < Convert.ToInt32(txt_hasta.Text))
                {

                    int? idp = null;

                    if (combo_proveedores.Items.FindByText("Sin selección").Selected == false)
                    {
                        idp=Convert.ToInt16(combo_proveedores.SelectedValue);
                    }

                    int? idmp = null;
                    if (combo_mp.Items.FindByText("Sin selección").Selected == false)
                    {
                        idmp = Convert.ToInt16(combo_mp.SelectedValue);
                    }

                    float? desde = null;
                    if (txt_desde.Text != "")
                    {
                        desde = float.Parse(txt_desde.Text);
                    }

                    float? hasta = null;
                    if (txt_desde.Text != "")
                    {
                        hasta = float.Parse(txt_hasta.Text);
                    }

                    if (desde > hasta)
                    {
                        hasta = null;
                        desde = null;
                    }
                    List<Compra> lista_compras = GestorInformeCompras.listarCompras(idp, idmp, desde, hasta);
                    grillaCompras.DataSource = null;
                    grillaCompras.DataSource = lista_compras;
                    string[] keys = new string[] { "Id" };
                    grillaCompras.DataKeyNames = keys;
                    grillaCompras.DataBind();
                    
                }
                    
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


        
    }
}