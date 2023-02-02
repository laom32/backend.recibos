using Aplicacion.Dtos;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.recibos.Controllers.v1
{
    public class ReciboController : BaseApiController
    {
        private readonly IReciboService _recibo;
        public ReciboController(IReciboService recibo)
        {
            _recibo = recibo;
        }
        // GET: api/<ReciboController>
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                return Ok(_recibo.GetList(pageNumber, pageSize));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<ReciboController>/5
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                return Ok(_recibo.Get(id));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ReciboController>
        [HttpPost]
        public IActionResult Post([FromBody] ReciboDto value)
        {
            try
            {
                return Ok(_recibo.Save(value));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ReciboController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReciboDto value)
        {
            try
            {
                value.Id = id;
                return Ok(_recibo.Update(value));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ReciboController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_recibo.ChangeInactive(id));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
