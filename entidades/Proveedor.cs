using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public long Cuit { get; set; }
        public int Vigente { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public Domicilio Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
