using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using daos;
using entidades;

namespace negocio
{
    public static class GestorPagos
    {
        public static List<PedidoEnvioView> obtenerPedidosPendientes()
        {
            return DaoPago.obtenerPedidosPendientes();
        }

        public static void registrarPagoLocal(int id,int id_estado)
        {
            DaoPago.registrarPagoLocal(id,id_estado);
        }

        public static void registrarPagoDelivery(int id,int id_estado)
        {
            DaoPago.registrarPagoDelivery(id,id_estado);
        }

        public static List<PedidoEnvioView> obtenerPedidosPagados()
        {
            return DaoPago.obtenerPedidosPagados();
        }

        public static int obtenerPedidoPorNroPedido(long nro_pedido)
        {
            return DaoPago.obtenerPedidoPorNroPedido(nro_pedido);
        }




    }
}
