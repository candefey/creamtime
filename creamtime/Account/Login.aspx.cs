using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using creamtime.Models;
using entidades;
using negocio;

namespace creamtime.Account
{
    public partial class Login : Page
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
                Response.Redirect("~/AbmCliente.aspx", false);
            }
            else
            {
                Response.Redirect("~/AbmCliente.aspx", false);
            }
        }
    }
}