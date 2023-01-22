using CustomerMicroservice.Models;
using CustomerMicroservice.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerMicroservice.Controllers
{
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _services;
        private static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CustomersController(ICustomerServices services, AutoMapper.IMapper _mapper)
        {
            _services = services;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Customers>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Customers>>> GetAll()
        {
            try
            {
                Log.Info("CustomerMicroservice:[GetAll] Operation read all");
                var result = await _services.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("BorrowingMicroservice: [GetAll] error 500" + ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOne(int id)
        {
            Log.Info("CustomerMicroservice:[GetOne] Operation read one. ID: " + id);
            var item = await _services.GetOne(id);
            if (item == null)
            {
                Log.Error("CustomerMicroservice:[GetOne] Customer not found");
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CustomersBaseModel value)
        {
            if (value == null)
            {
                Log.Error("CustomerMicroservice:[Post] Create fields are null");
                return BadRequest("Customer null");
            }
            var id = await _services.Create(value);

            Log.Info("CustomerMicroservice:[Post] Operation create");
            return CreatedAtAction(nameof(GetAll), new { id = id }, id);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] CustomersBaseModel value)
        {
            var author = await _services.GetOne(id);


            if (author == null)
            {
                Log.Error("CustomerMicroservice:[Put] Customer not found");
                return NotFound("Customer Not Found");
            }

            try
            {
                Log.Info("CustomerMicroservices:[Put] Operation update one. ID: " + id);
                await _services.Update(id, value);

            }
            catch (Exception)
            {
                Log.Error("CustomerMicroservice:[Put] Customer not found");
                return NotFound("Customer Not Found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var customerItem = _services.GetOne(id);
            if (customerItem == null)
            {
                Log.Error("CustomerMicroservice:[Delete] Customer not found");
                return NotFound("Customer Not Found");
            }
            else
            {
                Log.Info("CustomerMicroservice:[Delete] Operation Delete");
                _services.Delete(id);
                return Ok();
            }
            

        }
    }
}
