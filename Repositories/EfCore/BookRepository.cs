using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApplicationContext context) : base(context)
        {
            
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trankChanges)
        {
            return await FindAll(trankChanges).OrderBy(x=>x.Id).ToListAsync();
                
        }

        public async Task<Book> GetOneBookByIdAsync(int id, bool trankChanges)
        {
            return await FindByCondition(x => x.Id == id,trankChanges).SingleOrDefaultAsync();

        }

        public void UpdateOneBook(Book book)=>Update(book);
    }
}
