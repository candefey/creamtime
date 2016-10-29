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
           
        }

        protected void LogIn(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Username = Email.Text;
            usuario.Password = Password.Text;
                
            if (GestorUsuario.login(usuario))
            {
                Session["user"] = usuario;
                Session["user_perm"] = GestorCliente.obtenerRolDeCliente(usuario.ClienteId);
                Response.Redirect("~/AbmCliente", false);
                
            }
            else
            {
                Response.Redirect("~/AbmCliente", false);
            }
        }
    }
}