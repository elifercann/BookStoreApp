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
            var books=_context.Books.ToList();
            return Ok(books);

        }
    }
}
