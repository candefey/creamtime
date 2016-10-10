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
            dao.insertarCliente(c);
        }

        public static List<Localidad> listarLocalidades()
        {
            return DaoDomicilio.listarLocalidades();
        }

        public static List<Barrio> listarBarrios(int id)
        {
            return DaoDomicilio.listarBarrios(id);
        }
        public static List<Sexo> listarSexo()
        {
            return DaoCliente.listarSexo();
        }
    }
}
