using daos;
using entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class GestorPedido
    {
        public static void registrarPedido(Pedido pedido, List<DetallePedido> detalles)
        {
            DaoPedido dao = new DaoPedido();
            dao.insertarPedido(pedido, detalles);
        }

        public static List<Pedido> informePedidos(DateTime? desde, DateTime? hasta, int? estado, string apellido)
        {
            return DaoPedido.informePedidos(desde, hasta, estado, apellido);
        }
    }
}
