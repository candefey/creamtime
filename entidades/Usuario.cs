using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class Usuario
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? ClienteId { get; set; }
    }
}
