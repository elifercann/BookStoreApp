using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Abstract
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters bookParameters,bool trankChanges);
        Task<Book> GetOneBookByIdAsync(int id,bool trankChanges);
        void CreateOneBook(Book book);
        void DeleteOneBook(Book book);
        void UpdateOneBook(Book book);
    }
}
