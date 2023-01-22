using BookMicroservice.Models;
using BookMicroservice.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;

namespace BookMicroservice.Controllers
{
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Route("api/Books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookServices _services;
        private static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BooksController(IBookServices services, AutoMapper.IMapper _mapper)
        {
            _services = services;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Books>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Books>>> GetAll()
        {

            try
            {
                Log.Info("BookMicroservice:[GetAll] Operation read all");
                var result = await _services.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("BookMicroservice: [GetAll] error 500"+ex); 
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Books), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOne(int id)
        {
            Log.Info("BookMicroservice:[GetOne] Operation read one. ID: " + id);
            var item = await _services.GetOne(id);
            if (item == null)
            {
                Log.Error("BookMicroservice:[GetOne] Book not found");
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] BooksBaseModel value)
        {
            if (value == null)
            {
                Log.Error("BookMicroservice:[Post] Create fields are null");
                return BadRequest("Book null");
            }
            Log.Info("BookMicroservice:[Post] Operation create");
            var id = await _services.Create(value);

            return CreatedAtAction(nameof(GetAll), new { id = id }, id);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] BooksBaseModel value)
        {

            var author = await _services.GetOne(id);


            if (author == null)
                return NotFound("Book Not Found");

            try
            {
                Log.Info("BookMicroservices:[Put] Operation update one. ID: " + id);
                await _services.Update(id, value);

            }
            catch (Exception)
            {
                Log.Error("BookMicroservice:[Put] Book not found");
                return NotFound("Book Not Found");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var bookItem = _services.GetOne(id);
            if (bookItem == null) { 
                Log.Error("BookMicroservice:[Delete] Book not found");
                return NotFound("Book Not Found");
            }
            else
            {
                Log.Info("BookMicroservice:[Delete] Operation delete");
                _services.Delete(id);
                return Ok();
            }
                

            
            
            



        }
    }
}
