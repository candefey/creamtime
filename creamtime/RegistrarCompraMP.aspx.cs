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
    public partial class RegistrarCompraMP : System.Web.UI.Page
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

            List<DetalleCompra> lista;

            if (Application["detalles"] != null)
            {
                lista = (List<DetalleCompra>)Application["detalles"];
            }
            else
            {
                lista = new List<DetalleCompra>();
            }


            grillaDetalles.DataSource = lista;
            grillaDetalles.DataBind();

            if (!Page.IsPostBack)
            {
                
                lbl_error_.Visible = false;
                lbl_success_.Visible = false;
                lbl_warning_.Visible = false;

                List<ProveedorView> proveedores = GestorProveedor.listarProveedores();
                combo_proveedores.DataSource = proveedores;
                combo_proveedores.DataTextField = "RazonSocial";
                combo_proveedores.DataValueField = "Id";
                combo_proveedores.DataBind();

                combo_proveedores.ClearSelection();
                combo_proveedores.Items.Add("Sin selección");
                combo_proveedores.Items.FindByText("Sin selección").Selected = true;
            }
        }

        protected void combo_proveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                int id = Convert.ToInt32(combo_proveedores.SelectedValue);
                combo_mp.DataSource = GestorNuevaCompra.listarMPFiltro(id);
                combo_mp.DataTextField = "Nombre";
                combo_mp.DataValueField = "Id";
                combo_mp.DataBind();

        }

        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            
                List<DetalleCompra> lista;

                if (Application["detalles"] != null)
                {
                    lista = (List<DetalleCompra>)Application["detalles"];
                }
                else
                {
                    lista = new List<DetalleCompra>();
                }


                int idp = Convert.ToInt16(combo_proveedores.SelectedValue);
                string pro = combo_proveedores.SelectedItem.Text;
                int idmp = Convert.ToInt16(combo_mp.SelectedValue);
                string mp = combo_mp.SelectedItem.Text;

                DetalleCompra detalle = new DetalleCompra();
                detalle.IdProveedor = idp;
                detalle.nombreProveedor = pro;
                detalle.nombreMP = mp;
                detalle.IdMP = idmp;
                detalle.Cantidad = Convert.ToInt32(txt_cantidad.Text);
                MateriaPrima materia = GestorNuevaCompra.buscarMateriaPrima(idmp);
                float monto = (float)materia.precio * detalle.Cantidad;
                detalle.Monto = monto;

                lista.Add(detalle);
                Application["detalles"] = lista;


        }

        protected void btn_confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                List<DetalleCompra> lista= (List<DetalleCompra>)Application["detalles"];
            GestorNuevaCompra.insertarCompra(lista);
            combo_proveedores.ClearSelection();
            combo_proveedores.Items.FindByText("Sin selección").Selected = true;

        }
            catch (Exception ex)
            {
                throw new ApplicationException("Error generico en la inserción.");
    }
            finally
            {
                Application["detalles"] = null;
            }
        }
    }
}