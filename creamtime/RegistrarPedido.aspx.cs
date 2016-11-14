using daos;
using entidades;
using negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace creamtime
{
    public partial class RegistrarPedido : System.Web.UI.Page
    {

        protected void Page_Prerender(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                if ((String)Session["user_perm"] == "Cliente")
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

            if (!Page.IsPostBack)
            {
                lbl_error.Visible = false;
                lbl_success.Visible = false;
                lbl_warning.Visible = false;

                cargarProductos();                
            }

        }


        protected void cargarGrilla()
        {
            List<DetallePedido> listaDetalles = (List<DetallePedido>)Session["listaDetalles"];

            grillaProductos.DataSource = listaDetalles;
            string[] keys = new string[] { "ID" };
            grillaProductos.DataKeyNames = keys;
            grillaProductos.DataBind();

        }


        protected void cargarProductos()
        {
            combo_producto.DataSource = GestorProducto.listaProductosVendibles();
            combo_producto.DataTextField = "Nombre";
            combo_producto.DataValueField = "Id";
            combo_producto.DataBind();
            combo_producto.Items.Add(new ListItem("Sin seleccion", "Todos"));
            combo_producto.Items.FindByText("Sin seleccion").Selected = true;
        }


        //Agrega un elemento al carrito de compras
        public void btn_agregarDetalle(object sender, EventArgs e)
        {
            DetallePedido detalle = new DetallePedido();
            Producto prod = new Producto();
            SubDetallePedido subdetalle;
            List<DetallePedido> listaDetalles;
            List<SubDetallePedido> listaSabores;
            int i = 1;
            
            //Seteo el detalle
            detalle.Cantidad = 1;
            prod.Id = int.Parse(combo_producto.SelectedValue);
            prod.Nombre = combo_producto.SelectedItem.Text;
            detalle.Producto = prod;
            detalle.Precio = GestorProducto.obtenerProductoPorID(prod.Id).Precio;

            //Verifica si el carrito esta vacio
            if (Session["listaDetalles"] != null)
            {
                //Si tiene elementos, los almacena en la lista para persistirlos
                listaDetalles = (List<DetallePedido>)Session["listaDetalles"];                
            }
            else
            {
                //Crea la lista de cero
                listaDetalles = new List<DetallePedido>();
            }

            //Seteo id de detalle provisiorio
            detalle.ID = listaDetalles.Count + 1;

            listaSabores = new List<SubDetallePedido>();

            //Recorro los sabores (en caso de que no haya no recorre nada)
            List<DropDownList> dropdowns = new List<DropDownList>();
            GetControlList<DropDownList>(Page.Controls, dropdowns);

            foreach (var combo in dropdowns)
            {
                string n = "ddl_" + i;
                if (combo.ID == n && combo.Visible == true)
                {
                    subdetalle = new SubDetallePedido();
                    prod = new Producto();

                    prod.Id = int.Parse(combo.SelectedValue);
                    prod.Nombre = combo.SelectedItem.Text;

                    subdetalle.Producto = prod;

                    listaSabores.Add(subdetalle);

                    i += 1;
                }
            }
            //Agrego sabores
            detalle.sabores = listaSabores;

            //Agrego el detalle
            listaDetalles.Add(detalle);
            Session.Add("listaDetalles", listaDetalles);

            //Limpio
            limpiar();
            cargarGrilla();
        }

        //Elimina un elemento del carrito de compras
        protected void btn_eliminar_Click(object sender, GridViewDeleteEventArgs e)
        {
            String keyCod = grillaProductos.DataKeys[Convert.ToInt32(e.RowIndex)].Value.ToString();
            int id = int.Parse(keyCod);
            List<DetallePedido> listaDetalles;
            DetallePedido eliminar = new DetallePedido();

            if (Session["listaDetalles"] != null)
            {
                listaDetalles = (List<DetallePedido>)Session["listaDetalles"];

                foreach (DetallePedido detalle in listaDetalles)
                {
                    if (detalle.ID == id)
                    {
                        eliminar = detalle;
                        break;
                    }
                }

                if (eliminar != null)
                    listaDetalles.Remove(eliminar);

                Session.Add("listaDetalles", listaDetalles);
            }

            cargarGrilla();
        }


        protected void btn_registrar_pedido_Click(object sender, EventArgs e)
        {
            //Verifico que haya cargado algun producto al carrito de compras
            if (Application["listaDetalles"] != null)
            {
                List<DetallePedido> listaDetalles = (List<DetallePedido>)Application["listaDetalles"];

                Pedido pedido = new Pedido();

                Usuario user = (Usuario) Session["user"];
                string username = user.Username;
                pedido.Cliente = GestorCliente.obtenerClientePorUsuario(username);
                pedido.Fecha_Pedido = DateTime.Now.Date;

                float monto = 0;
                foreach (DetallePedido detalle in listaDetalles)
                {
                    monto += detalle.Precio;
                }

                pedido.Monto = monto;

                if (chk_envio.Checked)
                    pedido.Estado = GestorEstados.obtenerEstadoPorNombre("Delivery");
                else
                    pedido.Estado = GestorEstados.obtenerEstadoPorNombre("Local");

                Random random = new Random();
                pedido.Nro_Pedido = random.Next();

                //REGISTRO EL PEDIDO
                try
                {
                    GestorPedido.registrarPedido(pedido, listaDetalles);

                    lbl_success.Text = "Pedido registrado con exito!";
                    lbl_success.Visible = true;
                    lbl_warning.Visible = false;
                    lbl_error.Visible = false;

                    Application.Clear();
                    cargarGrilla();
                    limpiar();

                }
                catch (Exception ex)
                {
                    lbl_error.Text = "Ha surgido un error inesperado";
                    lbl_error.Visible = true;
                }

            }
            else
            {
                lbl_warning.Text = "Debe ingresar algun producto al carrito de compras";
                lbl_warning.Visible = true;
            }
        }


        protected void limpiar()
        {
            lbl_sabores.Visible = false;
            //Limpia combos
            List<DropDownList> dropdowns = new List<DropDownList>();
            GetControlList<DropDownList>(Page.Controls, dropdowns);

            int i = 1;
            foreach (var combo in dropdowns)
            {
                string n = "ddl_" + i;
                if (combo.ID == n && combo.Visible == true)
                {
                    combo.Visible = false;
                    i += 1;
                }
            }

            contenedorSabores.Visible = false;
        }

        protected void combo_producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_producto.SelectedValue != "Todos")
            {
                int id = int.Parse(combo_producto.SelectedValue);

                int cantidad = GestorProducto.obtenerAgregados(id);

                List<Producto> sabores = GestorProducto.listaSabores();

                //Limpio previo a mostrar
                limpiar();

                contenedorSabores.Visible = true;

                for (int i = 1; i < cantidad; i++)
                {
                    List<DropDownList> dropdowns = new List<DropDownList>();
                    GetControlList<DropDownList>(Page.Controls, dropdowns);

                    string nombre = "ddl_" + i;

                    foreach (var combo in dropdowns)
                    {
                        if (combo.ID == nombre)
                        {
                            combo.DataSource = sabores;
                            combo.DataTextField = "Nombre";
                            combo.DataValueField = "ID";
                            combo.DataBind();

                            combo.Visible = true;
                        }
                    }
                }
                if (cantidad > 0)
                    lbl_sabores.Visible = true;
            }
            else
            {
                contenedorSabores.Visible = false;
            }
                
        }


        private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
        where T : Control
        {
            foreach (Control control in controlCollection)
            {
                //if (control.GetType() == typeof(T))
                if (control is T) // This is cleaner
                    resultCollection.Add((T)control);

                if (control.HasControls())
                    GetControlList(control.Controls, resultCollection);
            }
        }
    }
}