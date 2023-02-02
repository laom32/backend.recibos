using Aplicacion.Dtos;
using Aplicacion.Interfaces;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Dominio.Entities;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Services
{
    public class ReciboService : IReciboService
    {
        readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private BackDbContext _context;
        public ReciboService(IUnitOfWork uow, IMapper mapper)
        {
            _uow= uow;
            _mapper= mapper;
            _context = (_uow as IUnitOfWork<BackDbContext>).DbContext;
        }
        
        
        public ResponseSave Save(ReciboDto dto)
        {
            ResponseSave response = new ResponseSave();
            try
            {
                var recibo = _mapper.Map<Recibo>(dto);
                recibo.Activo = true;
                _uow.GetRepository<Recibo>().Insert(recibo);
                _uow.SaveChanges();

                response.Id = recibo.Id;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error al guardar el recibo.");
            }
        }
        
        public ResponseGeneric Update(ReciboDto dto)
        {
            ResponseGeneric response = new ResponseGeneric();
            try
            {
                var recibo = _mapper.Map<Recibo>(dto);
                _context.Recibos.Attach(recibo);
                _context.Entry(recibo).Property(x => x.Proveedor).IsModified = true;
                _context.Entry(recibo).Property(x => x.Monto).IsModified = true;
                _context.Entry(recibo).Property(x => x.Moneda).IsModified = true;
                _context.Entry(recibo).Property(x => x.Fecha).IsModified = true;
                _context.Entry(recibo).Property(x => x.Comentario).IsModified = true;

                _uow.SaveChanges();
                response.IsSuccess = true;

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al guardar el recibo. {ex.Message}");
            }
        }
        
        public ResponseGetRecibo Get(int reciboId)
        {
            ResponseGetRecibo response = new ResponseGetRecibo();
            try
            {

                var recibo = _uow.GetRepository<Recibo>().GetFirstOrDefault(predicate: x => x.Id == reciboId && x.Activo);
                if(recibo== null)
                {
                    response.IsSuccess = false;
                    response.Msg = $"El recibo solicitado no existe o no está activo.";
                    return response;
                }
                response.Recibo = _mapper.Map<ReciboDto>(recibo);
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un problema, favor de contactar al administrador del sistema. {ex.Message}");
            }
        }
        public ResponseGetListRecibos GetList(int pageIndex=1, int pageSize=1000)
        {
            if(pageIndex < 0)
            {
                pageIndex = 1;
            }
            if (pageSize < 0)
            {
                pageSize = 0;
            }
            ResponseGetListRecibos response = new ResponseGetListRecibos();
            try
            {
                //var recibos = _uow.GetRepository<Recibo>().GetPagedList(predicate: x => x.Activo, pageIndex: pageIndex, pageSize: pageSize, orderBy: c);
                var totalCount = _context.Recibos.Count();
                var recibos = _context.Recibos.OrderBy(o => o.Proveedor).Where(predicate: x => x.Activo)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
                response.Recibos = _mapper.Map<List<ReciboDto>>(recibos);
                response.Total = totalCount;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un problema, favor de contactar al administrador del sistema.");
            }
        }
        public ResponseGeneric ChangeInactive(int reciboId)
        {
            ResponseGeneric response = new ResponseGeneric();
            try
            {
                Recibo recibo = new Recibo();
                recibo.Id = reciboId;
                recibo.Activo = false;
                _context.Recibos.Attach(recibo);
                _context.Entry(recibo).Property(x => x.Activo).IsModified = true;

                _uow.SaveChanges();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        public ResponseGeneric Delete(int reciboId)
        {
            ResponseGeneric response = new ResponseGeneric();
            try
            {
                _uow.GetRepository<Recibo>().Delete(reciboId);
                _uow.SaveChanges();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
    }
}
