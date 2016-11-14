using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace creamtime
{
    public partial class InformePedidos : System.Web.UI.Page
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
            cargarGrilla();
            if(!Page.IsPostBack)
            {
                cargarCombo();
                cargarGrilla();
            }
        }

        
        protected void cargarCombo()
        {
            combo_estado.DataSource = GestorEstados.obtenerEstados();
            combo_estado.DataTextField = "Nombre";
            combo_estado.DataValueField = "Id";
            combo_estado.DataBind();

            combo_estado.Items.Add(new ListItem("Sin seleccion", "Todos"));
            combo_estado.Items.FindByText("Sin seleccion").Selected = true;
        }


        protected void cargarGrilla()
        {
            DateTime? desde = null;
            DateTime? hasta = null;
            string apellido = null;
            int? estado = null;

            if (!string.IsNullOrWhiteSpace(txt_fecha_desde.Text))
                desde = Convert.ToDateTime(txt_fecha_desde.Text);
            

            if (!string.IsNullOrWhiteSpace(txt_fecha_hasta.Text))
                hasta = Convert.ToDateTime(txt_fecha_hasta.Text);
            
            if (combo_estado.SelectedValue != "Todos" && combo_estado.SelectedValue != "")
                estado = int.Parse(combo_estado.SelectedValue);

            if (!string.IsNullOrWhiteSpace(txt_apellido_cliente.Text))
                apellido = txt_apellido_cliente.Text;
            
            //RECUPERO LOS PEDIDOS
            grillaPedidos.DataSource = GestorPedido.informePedidos(desde, hasta, estado, apellido);
            grillaPedidos.DataBind();
        }


        protected void btn_filtrar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }


    }
}