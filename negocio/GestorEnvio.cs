using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidades;
using daos;

namespace negocio
{
    public static class GestorEnvio
    {
        public static List<PedidoEnvioView> obtenerEnviosPendientes()
        {
            return DaoEnvio.obtenerEnviosPendientes();
        }

        public static void registrarEnvio(Envio env)
        { 
            DaoEnvio.registrarEnvio(env);
        }

       public static int obtenerPedidoPorNroPedido(long nro)
        {
            int id = DaoEnvio.obtenerPedidoPorNroPedido(nro);
            return id;

        }
        
        public static List<Cliente> obtenerRepartidores()
        {
            return DaoEnvio.obtenerRepartidores();
        }

        public static Estado obtenerEstadoPorNombre(string nombre)
        {
            return DaoEnvio.obtenerEstadoPorNombre(nombre);
        }

        public static List<EnviosView> obtenerEnviosRealizados()
        {
            return DaoEnvio.obtenerEnviosRealizados();
        }


    }
}
