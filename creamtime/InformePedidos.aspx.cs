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
            DateTime desde = new DateTime();
            DateTime hasta = new DateTime();
            string apellido;
            int estado = -1;

            if (!string.IsNullOrWhiteSpace(txt_fecha_desde.Text))
                desde = Convert.ToDateTime(txt_fecha_desde.Text);
            else
                desde = Convert.ToDateTime("01/01/1900");

            if (!string.IsNullOrWhiteSpace(txt_fecha_hasta.Text))
                hasta = Convert.ToDateTime(txt_fecha_hasta.Text);
            else
                hasta = Convert.ToDateTime("31/12/3999");

            if (!string.IsNullOrWhiteSpace(txt_apellido.Text))
                apellido = txt_apellido.Text;
            else
                apellido = "";

            if (combo_estado.SelectedValue != "Todos")
                estado = int.Parse(combo_estado.SelectedValue);

            grillaPedidos.DataSource = GestorPedido.informePedidos(desde, hasta, apellido, estado);
            grillaPedidos.DataBind();
        }


        protected void btn_filtrar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }


    }
}