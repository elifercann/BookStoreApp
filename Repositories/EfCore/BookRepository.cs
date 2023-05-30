using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;

namespace Repositories.EfCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApplicationContext context) : base(context)
        {
            
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters bookParameters,bool trankChanges)
        {
            return await FindAll(trankChanges).OrderBy(x=>x.Id).Skip((bookParameters.PageNumber-1)*bookParameters.PageSize).Take(bookParameters.PageSize).ToListAsync();
                
        }

        public async Task<Book> GetOneBookByIdAsync(int id, bool trankChanges)
        {
            return await FindByCondition(x => x.Id == id,trankChanges).SingleOrDefaultAsync();

        }

        public void UpdateOneBook(Book book)=>Update(book);
    }
}
