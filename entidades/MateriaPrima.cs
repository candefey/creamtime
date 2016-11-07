using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class MateriaPrima
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProveedor { get; set; }
        public float precio { get; set; }
    }
}
