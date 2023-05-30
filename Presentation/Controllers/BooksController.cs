﻿using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Abstract;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
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
        public async Task<IActionResult> GetAllBooksAsync([FromQuery]BookParameters bookParameters)
        {
            var books = await _manager.BookService.GetAllBooksAsync(bookParameters,false);
            return Ok(books);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute] int id)
        {

            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);

            return Ok(book);
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {

            var book = await _manager.BookService.CreateOneBookAsync(bookDto);

            return StatusCode(201, book);

        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
        {

            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);//takibi managerda yapıldığı için burada yapılmasına gerek yok
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);

            return NoContent();

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);
            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);
            return NoContent();

        }
    }
}
