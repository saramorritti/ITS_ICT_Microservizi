using BorrowingMicroservice.Models;
using BorrowingMicroservice.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BorrowingMicroservice.Controllers
{
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Route("api/Borrowings")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingServices _services;
        private static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BorrowingsController(IBorrowingServices services, AutoMapper.IMapper _mapper)
        {
            _services = services;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Borrowings>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Borrowings>>> GetAll()
        {
            try
            {
                Log.Info("BorrowingMicroservice:[GetAll] Read all succeded");
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
        [ProducesResponseType(typeof(Borrowings), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOne(int id)
        {
            Log.Info("BorrowingMicroservice:[GetOne] Read one succeded. ID: " + id);
            var item = await _services.GetOne(id);
            if (item == null)
            {
                Log.Error("BorrowingMicroservice:[GetOne] Borrowing not found");
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] BorrowingsBaseModel value)
        {
            if (value == null)
            {
                Log.Error("BorrowingMicroservice:[Post] Create fields are null");
                return BadRequest("Borrowing null");
            }
            Log.Info("BorrowingMicroservice:[Post] Create succeded");
            var id = await _services.Create(value);

            return CreatedAtAction(nameof(GetAll), new { id = id }, id);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] BorrowingsBaseModel value)
        {
            var borrow = await _services.GetOne(id);


            if (borrow == null)
                return NotFound("Borrowing Not Found");

            try
            {
                Log.Info("BorrowingMicroservices:[Put] Update one succeded. ID: " + id);
                await _services.Update(id, value);

            }
            catch (Exception)
            {

                Log.Error("BorrowingMicroservice:[Put] Borrowing not found");
                return NotFound("Borrowing Not Found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult Delete(int id)
        {
            var borrowingItem = _services.GetOne(id);
            if (borrowingItem == null)
            {
                Log.Error("BorrowingMicroservice:[Delete] Borrowing not found");
                return NotFound("Borrowing Not Found");
            }
            else
            {
                Log.Info("BorrowingMicroservice:[Delete] Delete succeded");
                _services.Delete(id);
                return Ok();
            }


        }
    }
}
