using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Domain.Entities;
using Cms.Domain.Interfaces;
using Cms.Service.Services;
using Cms.Service.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cms.Application.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {

        private readonly IAgendaService _service;
    

        public AgendaController(IAgendaService service)
        {
            _service = service;          
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _service.Get();
            return Ok(lista);
        }

        public IActionResult Post([FromBody] Object value)
        {
            try
            {               
                var item= _service.Post(value.ToString());
                return new ObjectResult(item.Id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Agenda obj = _service.Get(id);
                if (obj == null) return NotFound();

                return Ok(obj);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut]
        public IActionResult Put([FromBody] Object value)
        {
            try
            {
               Agenda item= _service.Put(value.ToString());
                return new ObjectResult(item);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
     
        [HttpDelete("{id}/{data}")]
        public IActionResult CancelarAgendamento(int id, string data)
        {
            try
            {
                _service.Delete(id, data);
                return new NoContentResult();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}