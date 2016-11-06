using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class Envio
    {
        public int ID { get; set; }
        public int Repartidor { get; set; }
        public DateTime Fecha_Partida { get; set; }
        public DateTime Fecha_Llegada { get; set; }
        public Pedido Pedido { get; set; }
        public Estado Estado { get; set; }
        public int Nro_Envio { get; set; }
    }
}
