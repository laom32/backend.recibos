using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dtos
{
    public class ResponseGetRecibo : ResponseGeneric
    {
        public ReciboDto Recibo { get; set; }
    }
}
