using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using creamtime.Models;
using entidades;
using negocio;

namespace creamtime.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                lbl_warning.Visible = false;
            }
            
        }

        protected void LogIn(object sender, EventArgs e)
        {
            lbl_warning.Visible = false;
            Usuario usuario = new Usuario();
            usuario.Username = Email.Text;
            usuario.Password = Password.Text;
                
            if (GestorUsuario.login(usuario))
            {
                Session["user"] = usuario;
                Session["user_perm"] = GestorCliente.obtenerRolDeCliente(usuario.ClienteId);
                Response.Redirect("~/", false);
                
            }
            else
            {
                lbl_warning.Text = "Error en el usuario y/contraseña, intente nuevamente";
                lbl_warning.Visible = true;
            }
        }
    }
}