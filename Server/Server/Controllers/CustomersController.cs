using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllAsync());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _service.GetByIdAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public IActionResult Insert(CustomerDto dto)
        {
            var customer = new Customer { FullName = dto.FullName, Email = dto.Email, Phone = dto.Phone };
            _service.InsertAsync(customer);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CustomerDto dto)
        {
            var updated = new Customer { Id = id, FullName = dto.FullName, Email = dto.Email, Phone = dto.Phone };
            _service.UpdateAsync(id, updated);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.DeleteAsync(id);
            return Ok();
        }
    }

}
