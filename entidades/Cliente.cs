using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long Dni { get; set; }
        public Rol Rol { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha_nacimiento { get; set; }
        public Boolean Vigente { get; set; }
        public Sexo Sexo { get; set; }
        public long Telefono { get; set; }
        public string Email { get; set; }
        public Domicilio Domicilio { get; set; }
    }
}
