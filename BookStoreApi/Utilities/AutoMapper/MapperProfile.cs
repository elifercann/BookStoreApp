using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace BookStoreApi.Utilities.AutoMapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<BookDtoForUpdate, Book>().ReverseMap();
          
            CreateMap<Book, BookDto>();
            CreateMap<BookDtoForInsertion, Book>();
           
        }
    }
}
