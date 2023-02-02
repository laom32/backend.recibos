using Aplicacion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IReciboService
    {
        /// <summary>
        /// Método para guardar el recibo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        ResponseSave Save(ReciboDto dto);
        /// <summary>
        /// Método para actualizar el recibo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        ResponseGeneric Update(ReciboDto dto);
        /// <summary>
        /// Método para obtener un recibo en específico.
        /// </summary>
        /// <param name="reciboId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        ResponseGetRecibo Get(int reciboId);
        /// <summary>
        /// Método para obtener el listado de recibos.
        /// </summary>
        /// <param name="pageIndex">Número de página</param>
        /// <param name="pageSize">Total de resultados</param>
        /// <returns></returns>
        ResponseGetListRecibos GetList(int pageIndex = 1, int pageSize = 1000);
        /// <summary>
        /// Método para poner como desactivado un recibo y que no se visualice en los resultados GET
        /// </summary>
        /// <param name="reciboId"></param>
        /// <returns></returns>
        ResponseGeneric ChangeInactive(int reciboId);
        /// <summary>
        /// Método para eliminar la información de un recibo de forma definitiva.
        /// </summary>
        /// <param name="reciboId"></param>
        /// <returns></returns>
        ResponseGeneric Delete(int reciboId);
    }
}
