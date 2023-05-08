using BookStoreApi.Models;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public BooksController(ApplicationContext context)
        {
            _context = context;

        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        [HttpGet("{id}")]
        public IActionResult GetOneBook([FromRoute] int id)
        {
            try
            {
                var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
                if (book == null)
                    return NotFound();
                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }


        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book == null)
                    return BadRequest();
                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
    }
}
