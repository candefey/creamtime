using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class DetalleCompra
    {
        
        public int IdMP { get; set; }
        public string nombreMP { get; set; }
        public int IdProveedor { get; set; }
        public string nombreProveedor { get; set; }
        public int Cantidad { get; set; }
        public float Monto { get; set; }
    }
}
