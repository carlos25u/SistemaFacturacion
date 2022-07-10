using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFacturacion.Entidades
{
    class Facturas
    {
        [Key]
        public int FacturaId { get; set; }
        public String Clientes { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int Cantidad { get; set; }
        public float Total { get; set; }
        public int ClienteId { get; set; }

        [ForeignKey("FacturaId")]
        public List<FacturasDetalles> Detalle { get; set; } = new List<FacturasDetalles>();

        [ForeignKey("ClienteId")]
        public Clientes clientes { get; set; }

    }
}
