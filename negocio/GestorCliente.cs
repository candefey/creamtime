using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using daos;
using entidades;

namespace negocio
{
    public static class GestorCliente
    {
        public static void insertarCliente(Cliente c)
        {
            DaoCliente dao = new DaoCliente();
            dao.insertarCliente(Cliente c);
        }
    }
}
