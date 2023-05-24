using Entities.DTOs;
using Entities.Models;

namespace Services.Abstract
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks(bool trackChanges);
        Book GetOneBookById(int id,bool trackChanges);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id,BookDtoForUpdate book, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
    }
}
