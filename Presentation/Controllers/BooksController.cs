using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);

        }

        [HttpGet("{id}")]
        public IActionResult GetOneBook([FromRoute] int id)
        {
            
            var book = _manager.BookService.GetOneBookById(id, false);
            
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {

            if (book == null)
                return BadRequest();
            _manager.BookService.CreateOneBook(book);

            return StatusCode(201, book);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateOneBook([FromRoute] int id, [FromBody] Book book)
        {

            if (book == null)
                return BadRequest();
            _manager.BookService.UpdateOneBook(id, book, true);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOneBook([FromRoute] int id)
        {
            _manager.BookService.DeleteOneBook(id, false);

            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var entity = _manager.BookService.GetOneBookById(id, true);//değişiklikler izleniyor
           
            bookPatch.ApplyTo(entity);
            _manager.BookService.UpdateOneBook(id, entity, true);

            return NoContent();

        }
    }
}
