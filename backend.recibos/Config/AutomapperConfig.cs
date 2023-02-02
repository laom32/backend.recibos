using Aplicacion.Dtos;
using Dominio.Entities;
using AutoMapper;

namespace backend.recibos.Config
{
    public class AutomapperConfig : AutoMapper.Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Recibo, ReciboDto>().ReverseMap();

        }
    }
}
