using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class Compra
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public float Monto { get; set; }
        public Int64 Nro { get; set; }
        public string NombreProveedor { get; set; }
    }
}
