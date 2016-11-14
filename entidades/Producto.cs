using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoProducto Tipo_Producto { get; set; }
        public int Codigo_Producto { get; set; }
        public float Precio { get; set; }
        public DateTime Fecha_Alta { get; set; }
        public Boolean Vigente { get; set; }
        public int Agregados { get; set; }
    }
}
