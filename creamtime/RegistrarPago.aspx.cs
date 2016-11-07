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
    public partial class RegistrarPago : System.Web.UI.Page
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
                lbl_pedido.Text = "";
                lbl_success.Visible = false;
                lbl_error.Visible = false;

                Estado estado_pagado = GestorEnvio.obtenerEstadoPorNombre("Pagado");
                Estado estado_rechazado = GestorEnvio.obtenerEstadoPorNombre("Rechazado");
                List<Estado> lista_estado = new List<Estado>();
                lista_estado.Add(estado_pagado);
                lista_estado.Add(estado_rechazado);
                combo_estado.DataSource = lista_estado;
                combo_estado.DataTextField = "Nombre";
                combo_estado.DataValueField = "Id";
                combo_estado.DataBind();

                pago_gridview.DataSource = GestorPagos.obtenerPedidosPendientes();
                string[] keys = new string[] { "NumeroPedido" };
                pago_gridview.DataKeyNames = keys;
                pago_gridview.DataBind();

                pagos_realizados_gridview.DataSource = GestorPagos.obtenerPedidosPagados();
                pagos_realizados_gridview.DataBind();



            }
        }

        protected void boton_pago_Click(object sender, EventArgs e)
        {
            long nro_pedido = Convert.ToInt64(Application["nro_pedido_pago"]);
            int id_estado = Convert.ToInt32(combo_estado.SelectedValue);
            try
            {
                int id = GestorPagos.obtenerPedidoPorNroPedido(nro_pedido);
                string estado = (String)Application["estado"];
                if(estado== "Local")
                {
                    GestorPagos.registrarPagoLocal(id,id_estado);
                    lbl_success.Visible = true;
                    lbl_success.Text = "Pago en Local Registrado exitosamente!";
                    pagos_realizados_gridview.DataSource = GestorPagos.obtenerPedidosPagados();
                    pagos_realizados_gridview.DataBind();
                }
                else
                {
                    if(estado== "Enviado")
                    {
                        GestorPagos.registrarPagoDelivery(id,id_estado);
                        lbl_success.Visible = true;
                        lbl_success.Text = "Pago en Domicilio Registrado exitosamente!";
                        pagos_realizados_gridview.DataSource = GestorPagos.obtenerPedidosPagados();
                        pagos_realizados_gridview.DataBind();
                    }

                }

                pago_gridview.DataSource = GestorPagos.obtenerPedidosPendientes();
                string[] keys = new string[] { "NumeroPedido" };
                pago_gridview.DataKeyNames = keys;
                pago_gridview.DataBind();
            }



            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
                lbl_error.Visible = true;
                lbl_error.Text = "Error al registrar pago";
                Application["nro_pedido_pago"] = null;
                Application["estado"] = null;
            }
            


        }

        protected void pago_gridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbl_success.Visible = false;
            lbl_error.Visible = false;


            lbl_pedido.Text = "";
            long nro_pedido = Convert.ToInt64(pago_gridview.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());
            Application["nro_pedido_pago"] = nro_pedido;
            string estado = pago_gridview.Rows[Convert.ToInt32(e.CommandArgument)].Cells[10].Text;
            Application["estado"] = estado;

            lbl_pedido.Text = "Pedido seleccionado: " + nro_pedido;
        }
    }
}