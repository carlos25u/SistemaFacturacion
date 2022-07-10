using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFacturacion.Entidades
{
    class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Telefono { get; set; }
        public String Direccion { get; set; }
    }
}
