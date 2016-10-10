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
        public static Boolean existeUsuario(Usuario usuario)
        {
            return DaoUsuario.existeUsuario(usuario);
        }
    }
}
