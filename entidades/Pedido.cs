using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class Pedido
    {
        public int ID { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public Cliente Cliente { get; set; }
        public float Monto { get; set; }
        public Usuario Vendedor { get; set; }
        public Estado Estado { get; set; }
        public DateTime? Fecha_Pago { get; set; }
        public int Nro_Pedido { get; set; }
    }
}
