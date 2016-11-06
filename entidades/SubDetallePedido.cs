using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class SubDetallePedido
    {
        public int ID { get; set; }
        public DetallePedido Detalle_Pedido { get; set; }
        public Producto Producto { get; set; }
    }
}
