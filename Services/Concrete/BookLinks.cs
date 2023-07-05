using Entities.DTOs;
using Entities.LinkModel;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Abstract;

namespace Services.Concrete;

public class BookLinks : IBookLinks
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<BookDto> _dataShaper;

    public BookLinks(LinkGenerator linkGenerator, IDataShaper<BookDto> dataShaper)
    {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }

    public LinkResponse TryGenerateLinks(IEnumerable<BookDto> bookDtos, string fields, HttpContext httpContext)
    {
        var shapedBooks=ShapeData(bookDtos, fields);
        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkedBooks(bookDtos, fields, httpContext, shapedBooks);
        return ReturnShapedBooks(shapedBooks);
    }

    private LinkResponse ReturnLinkedBooks(IEnumerable<BookDto> bookDtos, string fields, HttpContext httpContext, List<Entity> shapedBooks)
    {
        var bookDtoList=bookDtos.ToList();
        for (int i = 0; i < bookDtoList.Count(); i++)
        {
            var bookLinks=CreateForBook(httpContext, bookDtoList[i],fields);
            shapedBooks[i].Add("Links", bookLinks);
        }

        var bookCollection = new LinkCollectionWrapper<Entity>(shapedBooks);
        return new LinkResponse { HasLinks=true,LinkedEntities=bookCollection};
    }

    private List<Link> CreateForBook(HttpContext httpContext, BookDto bookDto, string fields)
    {
        var links = new List<Link>()
        {
            new Link("a1","b1","c1"),
            new Link("a2","b2","c2")

        };
        return links;
    }

    private LinkResponse ReturnShapedBooks(List<Entity> shapedBooks)
    {
        return new LinkResponse() { ShapedEntities = shapedBooks };
    }

    private bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType =(MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
        return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }

    private List<Entity> ShapeData(IEnumerable<BookDto> bookDtos, string fields)
    {
       return _dataShaper.ShapeData(bookDtos, fields).Select(b=>b.Entity).ToList();
    }
}
