using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class ProductoView
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreTipo { get; set; }
        public int CodigoProducto { get; set; }
        public string Precio { get; set; }
        public string Vigente { get; set; }
    }
}
