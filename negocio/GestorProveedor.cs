using daos;
using entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class GestorProveedor
    {
        public static void insertarProveedor(Proveedor p)
        {
            DaoProveedor dao = new DaoProveedor();
            dao.insertarProveedor(p);
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
