using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class PedidoEnvioView
    {
        public long NumeroPedido { get; set; }
        public String NombreCliente { get; set; }
        public String ApellidoCliente { get; set; }
        public String Localidad { get; set; }
        public String Barrio { get; set; }
        public String Calle { get; set; }
        public String Numero { get; set; }
        public float Monto { get; set; }
        public DateTime FechaPedido  { get; set; }
        public String Estado { get; set; }
    }
}
