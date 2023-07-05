using Entities.DTOs;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Abstract;

public interface IBookLinks
{
    LinkResponse TryGenerateLinks(IEnumerable<BookDto> bookDtos, string fields, HttpContext httpContext);
}
