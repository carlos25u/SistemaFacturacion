using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFacturacion.Entidades
{
    class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }

    }
}
