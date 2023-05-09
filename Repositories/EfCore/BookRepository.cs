using Entities.Models;
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

        public IQueryable<Book> GetAllBooks(bool trankChanges)
        {
            return FindAll(trankChanges).OrderBy(x=>x.Id);
                
        }

        public Book GetOneBookById(int id, bool trankChanges)
        {
            return FindByCondition(x => x.Id == id,trankChanges).SingleOrDefault();

        }

        public void UpdateOneBook(Book book)=>Update(book);
    }
}
