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
    public partial class InformeClientes : System.Web.UI.Page
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
               

                List<Localidad> localidades = GestorCliente.listarLocalidades();
                combo_localidad.DataSource = localidades;
                combo_localidad.DataTextField = "Nombre";
                combo_localidad.DataValueField = "Id";
                combo_localidad.DataBind();
                combo_localidad.ClearSelection();
                combo_localidad.Items.Add("Sin selección");
                combo_localidad.Items.FindByText("Sin selección").Selected = true;


                Localidad cba = localidades.Find(Localidad => Localidad.Nombre == "Cordoba");
                combo_barrio.DataSource = GestorCliente.listarBarrios(cba.Id);
                combo_barrio.DataTextField = "Nombre";
                combo_barrio.DataValueField = "Id";
                combo_barrio.DataBind();
                combo_barrio.ClearSelection();
                combo_barrio.Items.Add("Sin selección");
                combo_barrio.Items.FindByText("Sin selección").Selected = true;


                //Carga de DropDown Sexo
                combo_sexo.DataSource = GestorCliente.listarSexo();
                combo_sexo.DataTextField = "Nombre";
                combo_sexo.DataValueField = "Id";
                combo_sexo.DataBind();
                combo_sexo.ClearSelection();
                combo_sexo.Items.Add("Sin selección");
                combo_sexo.Items.FindByText("Sin selección").Selected = true;
            }
        }

        protected void boton_informe_Click(object sender, EventArgs e)
        {
            string nombre_sexo = combo_sexo.SelectedItem.Text;
            string nombre_localidad = combo_localidad.SelectedItem.Text;
            string fecha_desde_texto = txt_fecha_desde.Text;
            string fecha_hasta_texto= txt_fecha_hasta.Text;           
            try
            {
                string nombre_sexo_parametro=null;
                if(combo_sexo.Items.FindByText("Sin selección").Selected== false)
                {
                    nombre_sexo_parametro = nombre_sexo;
                }

                string nombre_localidad_parametro = null;
                if (combo_localidad.Items.FindByText("Sin selección").Selected == false)
                {
                    nombre_localidad_parametro = nombre_localidad;
                }
                DateTime fecha_desde_parametro = new DateTime(1900, 1, 1);
                if (fecha_desde_texto != "")
                {
                    fecha_desde_parametro= Convert.ToDateTime(fecha_desde_texto);
                }
                DateTime fecha_hasta_parametro = DateTime.Today; 
                if (fecha_hasta_texto != "")
                {
                    fecha_hasta_parametro = Convert.ToDateTime(fecha_hasta_texto);
                }
                List<ClienteView> clientes = GestorCliente.obtenerClientesInforme(nombre_sexo_parametro, nombre_localidad_parametro, fecha_desde_parametro, fecha_hasta_parametro);
                info_clientes_gridview.DataSource = clientes;
                info_clientes_gridview.DataBind();

            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("Error en el listado");
            }
        }

        protected void combo_localidad_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(combo_localidad.SelectedItem.Text != "Sin selección")
                {
                int id = Convert.ToInt32(combo_localidad.SelectedValue);
                combo_barrio.DataSource = GestorCliente.listarBarrios(id);
                combo_barrio.DataTextField = "Nombre";
                combo_barrio.DataValueField = "Id";
                combo_barrio.DataBind();

                }
            else
            {
                combo_barrio.Items.Clear();
                combo_barrio.Items.Add("Sin selección");
                combo_barrio.Items.FindByText("Sin selección").Selected = true;
            }
           
        }
    }
}