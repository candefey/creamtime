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

            DaoCliente.insertarCliente(c);
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

        public static Boolean existeCliente(int dni)
        {
            return DaoCliente.existeCliente(dni);
        }

        public static string obtenerRolDeCliente(int? id)
        {
            return DaoCliente.obtenerRolDeCliente(id);
        }

        public static List<ClienteView> obtenerClientes()
        {
            return DaoCliente.obtenerClientes();
        }

        public static Cliente obtenerClientesDni(int dni)
        {
            return DaoCliente.obtenerClienteDni(dni);
        }

        public static void actualizarCliente(Cliente cliente)
        {
            DaoCliente.actualizarCliente(cliente);
        }

        public static void eliminarCliente(int dni)
        {
            DaoCliente.eliminarCliente(dni);
        }

        public static Cliente obtenerClientePorUsuario(string usuario)
        {
            return DaoCliente.obtenerClienteUsuario(usuario);
        }
        public static List<ClienteView> obtenerClientesInforme(string combo_sexo_texto, string localidad, string barrio, DateTime fecha_desde, DateTime fecha_hasta)
        {
            return DaoCliente.obtenerClientesInforme(combo_sexo_texto, localidad, barrio, fecha_desde, fecha_hasta);
        }

    }
}
