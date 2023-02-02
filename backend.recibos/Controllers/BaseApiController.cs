using Arch.EntityFrameworkCore.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.recibos.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private IUnitOfWork _uow;
        public BaseApiController()
        {

        }
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService(typeof(IMediator)) as IMediator;

        protected IUnitOfWork Uow => _uow ??= HttpContext.RequestServices.GetRequiredService(typeof(IUnitOfWork)) as IUnitOfWork;
    }
}
