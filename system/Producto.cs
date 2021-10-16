using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
    }
}