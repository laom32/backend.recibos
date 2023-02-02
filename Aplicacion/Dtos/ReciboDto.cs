using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dtos
{
    public class ReciboDto
    {
        public int Id { get; set; }
        public string Proveedor { get; set; }
        public decimal Monto { get; set; }
        public TipoMoneda Moneda { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
    }
}
