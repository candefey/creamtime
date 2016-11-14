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
    public partial class AbmProducto : System.Web.UI.Page
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
            cargarGrillaProductos();
            if (!IsPostBack)
            {
                lbl_error.Visible = false;
                lbl_success.Visible = false;
                lbl_warning.Visible = false;

                cargarTiposProductos();

                cargarGrillaProductos();
            }
            
        }

        protected void cargarTiposProductos()
        {
            combo_tipo_producto.DataSource = GestorProducto.listarTiposProductos();
            combo_tipo_producto.DataTextField = "Nombre";
            combo_tipo_producto.DataValueField = "Id";
            combo_tipo_producto.DataBind();
        }

        protected void cargarGrillaProductos()
        {
            grillaProductos.DataSource = GestorProducto.listarProductos("");
            string[] keys = new string[] { "CodigoProducto" };
            grillaProductos.DataKeyNames = keys;
            grillaProductos.DataBind();
        }

        protected void btn_producto_registrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    int codigo = int.Parse(txt_codigo_producto.Text);
                    if (GestorProducto.existeProducto(codigo))
                        throw new ApplicationException("Codigo");

                    Producto producto = new Producto();

                    producto.Nombre = txt_producto_nombre.Text;

                    TipoProducto tipo = new TipoProducto();
                    tipo.Id = int.Parse(combo_tipo_producto.SelectedValue);
                    tipo.Nombre = combo_tipo_producto.SelectedItem.Text;

                    producto.Tipo_Producto = tipo;

                    if (!string.IsNullOrWhiteSpace(txt_agregados.Text))
                        producto.Agregados = int.Parse(txt_agregados.Text);
                    else
                        producto.Agregados = 0;

                    Debug.WriteLine(producto.Agregados);

                    producto.Codigo_Producto = codigo;

                    producto.Precio = float.Parse(txt_precio.Text);

                    producto.Fecha_Alta = Convert.ToDateTime(txt_fecha_alta.Text);

                    producto.Vigente = chk_vigente.Checked;

                    GestorProducto.registrarProducto(producto);

                    lbl_success.Text = "Producto registrado con exito!";
                    lbl_success.Visible = true;
                    lbl_warning.Visible = false;
                    lbl_error.Visible = false;

                    cargarGrillaProductos();
                    limpiar();
                }
                catch (ApplicationException ap)
                {
                    if (ap.Message == "Codigo")
                    {
                        lbl_warning.Text = "Atencion! El código de producto ya existe, utilice otro";
                        lbl_warning.Visible = true;
                        txt_codigo_producto.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    lbl_error.Text = "Ha surgido un error inesperado, contacte a su administrador";
                    lbl_error.Visible = true;
                }
            }
        }


        protected void btn_eliminar_Click(object sender, GridViewDeleteEventArgs e)
        {
            String keyCod = grillaProductos.DataKeys[Convert.ToInt32(e.RowIndex)].Value.ToString();
            int codigo = int.Parse(keyCod);

            try
            {
                if (GestorProducto.eliminarProducto(codigo))
                {
                    lbl_success.Text = "Producto Eliminado con exito!";
                    lbl_success.Visible = true;
                }
                else
                {
                    lbl_error.Text = "No existe el producto a eliminar";
                    lbl_error.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Ha surgido un error inesperado, contacte a su administrador";
                lbl_error.Visible = true;
            }

            cargarGrillaProductos();
        }

        protected void grillaProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = e.CommandArgument.GetHashCode();
            String keyCod = grillaProductos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            int codigo = int.Parse(keyCod);

            Producto producto = GestorProducto.buscarProducto(codigo);

            txt_producto_nombre.Text = "" + producto.Nombre;
            combo_tipo_producto.SelectedItem.Text = producto.Tipo_Producto.Nombre;

            txt_agregados.Text = Convert.ToString(producto.Agregados);            

            txt_codigo_producto.Text = "" + producto.Codigo_Producto.ToString("D8");
            txt_precio.Text = "" + producto.Precio;
            txt_fecha_alta.Text = Convert.ToString(producto.Fecha_Alta.ToShortDateString());
            chk_vigente.Checked = producto.Vigente;

            //muestro botones
            btn_producto_guardar.Visible = true;
            btn_producto_registrar.Visible = false;
        }


        protected void btn_producto_guardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Producto producto = new Producto();

                    producto.Nombre = txt_producto_nombre.Text;

                    TipoProducto tipo = new TipoProducto();
                    tipo.Id = int.Parse(combo_tipo_producto.SelectedValue);
                    tipo.Nombre = combo_tipo_producto.SelectedItem.Text;

                    producto.Tipo_Producto = tipo;

                    if (!string.IsNullOrWhiteSpace(txt_agregados.Text))
                        producto.Agregados = int.Parse(txt_agregados.Text);

                    int codigo = int.Parse(txt_codigo_producto.Text);
                    producto.Codigo_Producto = codigo;

                    producto.Precio = float.Parse(txt_precio.Text);

                    producto.Fecha_Alta = Convert.ToDateTime(txt_fecha_alta.Text);

                    producto.Vigente = chk_vigente.Checked;

                    
                    if (GestorProducto.modificarProducto(producto))
                    {
                        lbl_success.Text = "Producto Modificado con exito!";
                        lbl_success.Visible = true;
                        lbl_warning.Visible = false;
                        lbl_error.Visible = false;
                    }
                    else
                    {
                        lbl_error.Text = "No existe el producto a modificar";
                        lbl_error.Visible = true;
                    }

                    cargarGrillaProductos();
                    limpiar();
                }
                catch (Exception ex)
                {
                    lbl_error.Text = "Ha surgido un error inesperado, contacte a su administrador";
                    lbl_error.Visible = true;
                }
            }

            btn_producto_guardar.Visible = false;
            btn_producto_registrar.Visible = true;
        }


        protected void limpiar()
        {
            txt_producto_nombre.Text = "";
            txt_agregados.Text = "";
            txt_codigo_producto.Text = "";
            txt_precio.Text = "";
            txt_fecha_alta.Text = "";
            chk_vigente.Checked = false;

            //muestro botones
            btn_producto_guardar.Visible = false;
            btn_producto_registrar.Visible = true;
        }
    }
}