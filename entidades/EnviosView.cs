using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class EnviosView
    {
        public String NombreRepartidor { get; set; }
        public String ApellidoRepartidor { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaLlegada { get; set; }
        public long NumeroEnvio { get; set; }
        public String Estado { get; set; }
    }
}
