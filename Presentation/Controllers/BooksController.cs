using Entities.DTOs;
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
        public IActionResult CreateOneBook([FromBody] BookDtoForInsertion bookDto)
        {

            if (bookDto == null)
                return BadRequest();
            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var book=_manager.BookService.CreateOneBook(bookDto);

            return StatusCode(201, book);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateOneBook([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
        {

            if (bookDto == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _manager.BookService.UpdateOneBook(id, bookDto, false);//takibi managerda yapıldığı için burada yapılmasına gerek yok

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOneBook([FromRoute] int id)
        {
            _manager.BookService.DeleteOneBook(id, false);

            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = _manager.BookService.GetOneBookForPatch(id, false);
           
            bookPatch.ApplyTo(result.bookDtoForUpdate,ModelState);
            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _manager.BookService.SaveChangesForPatch(result.bookDtoForUpdate,result.book);
            return NoContent();

        }
    }
}
