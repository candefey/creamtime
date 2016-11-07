using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using entidades;

namespace creamtime
{
    public partial class RegistrarEnvio : System.Web.UI.Page
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
                lbl_success.Visible = false;
                lbl_error.Visible = false;


                envio_gridview.DataSource = GestorEnvio.obtenerEnviosPendientes();
                string[] keys = new string[] { "NumeroPedido" };
                envio_gridview.DataKeyNames = keys;
                envio_gridview.DataBind();

                combo_repartidor.DataSource = GestorEnvio.obtenerRepartidores();
                combo_repartidor.DataTextField = "Nombre";
                combo_repartidor.DataValueField = "Id";
                combo_repartidor.DataBind();

                combo_demora.Items.Add(new ListItem("10"));
                combo_demora.Items.Add(new ListItem("15"));
                combo_demora.Items.Add(new ListItem("20"));
                combo_demora.Items.Add(new ListItem("25"));
                combo_demora.Items.Add(new ListItem("30"));
                combo_demora.Items.Add(new ListItem("35"));
                combo_demora.Items.Add(new ListItem("40"));
                combo_demora.Items.Add(new ListItem("45"));
                combo_demora.Items.Add(new ListItem("50"));
                combo_demora.Items.Add(new ListItem("55"));
                combo_demora.Items.Add(new ListItem("60"));

                envios_realizados_gridview.DataSource = GestorEnvio.obtenerEnviosRealizados();
                envios_realizados_gridview.DataBind();

                lbl_envio.Text = "";

            }

        }

        protected void envio_gridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbl_success.Visible = false;
            lbl_error.Visible = false;


            lbl_envio.Text = "";
            long nro_pedido = Convert.ToInt64(envio_gridview.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());
            Application["nro_pedido"] = nro_pedido;

            lbl_envio.Text = "Envio seleccionado: " + nro_pedido; 
        }

        protected void boton_envio_Click(object sender, EventArgs e)
        {
            long nro_pedido = Convert.ToInt64(Application["nro_pedido"]);
            try
            {

                int id = GestorEnvio.obtenerPedidoPorNroPedido(nro_pedido);
                Envio envio = new Envio();
                Pedido pedido = new Pedido();
                pedido.ID = id;
                envio.Pedido = pedido;
                envio.Repartidor = Convert.ToInt32(combo_repartidor.SelectedValue);
                envio.Nro_Envio = nro_pedido;
                Estado enviando = GestorEnvio.obtenerEstadoPorNombre("En camino");
                envio.Estado = enviando;
                envio.Fecha_Partida = DateTime.Now;
                envio.Fecha_Llegada = envio.Fecha_Partida.AddMinutes(Convert.ToInt32(combo_demora.SelectedValue));
                GestorEnvio.registrarEnvio(envio);

                envio_gridview.DataSource = GestorEnvio.obtenerEnviosPendientes();
                string[] keys = new string[] { "NumeroPedido" };
                envio_gridview.DataKeyNames = keys;
                envio_gridview.DataBind();

                envios_realizados_gridview.DataSource = GestorEnvio.obtenerEnviosRealizados();
                envios_realizados_gridview.DataBind();
                lbl_success.Visible = true;
                lbl_success.Text = "El pedido esta en camino!";
                Application["nro_pedido"] = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                lbl_error.Visible = true;
                lbl_error.Text = "Ocurrio un error en el registro del envio";
                Application["nro_pedido"] = null;
            }
            
        }
    }
}