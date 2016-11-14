﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidades
{
    public class DetalleCompra
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdMP { get; set; }
        public int IdProveedor { get; set; }
        public int Cantidad { get; set; }
        public float Monto { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreMP { get; set; }
    }
}

