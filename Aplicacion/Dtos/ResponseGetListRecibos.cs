using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aplicacion.Dtos
{
    public class ResponseGetListRecibos : ResponseGeneric
    {
        public IList<ReciboDto> Recibos { get; set; }
        public int Total { get; set; }
    }
}
