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

namespace Cms.Application.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase  
    {
        private readonly IService<Cliente> _service;

        public ClientesController(IService<Cliente> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _service.Get();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Cliente cliente = _service.Get(id);
                if (cliente == null) return NotFound();

                return Ok(cliente);
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

        [HttpPost]
        public IActionResult Post([FromBody] Cliente item)
        {
            try
            {

                _service.Post<ClienteValidator>(item);
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


        [HttpPut]
        public IActionResult Put([FromBody] Cliente item)
        {
            try
            {
                _service.Put<ClienteValidator>(item);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                Cliente obj = _service.Get(id);
                if (obj == null) return NotFound();
                _service.Delete(obj);

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