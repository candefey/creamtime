using daos;
using entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class GestorEstados
    {
        public static Estado obtenerEstadoPorNombre(string nombre)
        {
            return DaoEstados.obtenerEstadoPorNombre(nombre);
        }
    }
}
