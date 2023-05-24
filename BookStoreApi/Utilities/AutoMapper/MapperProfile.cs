using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace BookStoreApi.Utilities.AutoMapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<BookDtoForUpdate, Book>();
            CreateMap<Book, BookDtoForUpdate>();
            CreateMap<Book, BookDto>();
           
        }
    }
}
