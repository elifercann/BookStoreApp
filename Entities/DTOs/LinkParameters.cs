using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs;

public record LinkParameters
{
    public BookParameters BookParameters { get; init; }
    public HttpContext GetHttpContext { get; init; }
}
