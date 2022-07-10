using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFacturacion.Entidades
{
    class FacturasDetalles
    {
        [Key]
        public int FacturaDetalleId { get; set; }
        public int ProductoId { get; set; }
        public int FacturaId { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey("ProductoId")]
        public Productos Productos { get; set; }
        public Facturas Facturas { get; set; }
    }
}
