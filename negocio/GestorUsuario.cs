using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidades;
using daos;

namespace negocio
{
    public class GestorUsuario
    {

        public static Usuario existeUsuario(Usuario usuario)
        {
            //Se considera la situacion en la que un usuario exista pero su cliente asociado se dio de baja, 
            //por lo que el usuario es utilizable

            Boolean flag = true;
            Usuario user_existe = DaoUsuario.existeUsuario(usuario);

            if (user_existe.Username != null)
            {
                flag = DaoCliente.esClienteVigente(user_existe.ClienteId);
            }

            if (flag && user_existe.Username==null)
            {
                user_existe = usuario;
            }
            if(!flag && user_existe.Username!=null)
            {
                user_existe.ClienteId = null;
                user_existe.Password = usuario.Password;
            }

            return user_existe;
           
        }

        public static Boolean login(Usuario usuario)
        {
            Boolean flag = false;
            Usuario user = DaoUsuario.login(usuario);
            if (usuario.ClienteId!=null)
            {
                flag = DaoCliente.esClienteVigente(user.ClienteId);
            }

            return flag;
            
        }

        public static Usuario retornarUsuario(Usuario usuario)
        {
            return DaoUsuario.existeUsuario(usuario);
        }
    }
}
