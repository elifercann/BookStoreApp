using Entities.Models;

namespace Repositories.Abstract
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        IQueryable<Book> GetAllBooks(bool trankChanges);
        Book GetOneBookById(int id,bool trankChanges);
        void CreateOneBook(Book book);
        void DeleteOneBook(Book book);
        void UpdateOneBook(Book book);
    }
}
