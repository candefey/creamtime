﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class Domicilio
    {
        public int? Id { get; set; }
        public string Calle { get; set; }
        public Barrio Barrio { get; set; }
        public string Numero { get; set; }
    }
}
