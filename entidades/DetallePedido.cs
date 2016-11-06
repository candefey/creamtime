using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class DetallePedido
    {
        public int ID { get; set; }
        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public List<SubDetallePedido> sabores { get; set; }
    }
}
