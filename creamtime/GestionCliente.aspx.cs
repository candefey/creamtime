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
            cliente_gridview.DataSource = GestorCliente.obtenerClientes();
            string[] keys = new string[] { "Dni" };
            cliente_gridview.DataKeyNames = keys;
            cliente_gridview.DataBind();
        }
        }

        protected void cliente_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            cliente_gridview.DataSource = null;
            cliente_gridview.DataSource = GestorCliente.listarSexo();
            string[] keys = new string[] { "Id" };
            cliente_gridview.DataKeyNames = keys;
            cliente_gridview.DataBind();
        }
    }
}