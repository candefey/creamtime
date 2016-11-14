using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidades;
using daos;

namespace negocio
{
    public class GestorRol
    {
        public static Rol obtenerRolPorNombre(string nombre)
        {
            return DaoRol.obtenerRolPorNombre(nombre);
        }
    }
}
