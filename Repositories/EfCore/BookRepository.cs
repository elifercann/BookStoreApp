﻿using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;
using Repositories.EfCore.Extensions;

namespace Repositories.EfCore
{
    //class son haline geldi kalitimla devralinmasini istenmedigi için sadece genisletme islemi yapilmasi istendigi sealed olarak isaretlendi
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApplicationContext context) : base(context)
        {
            
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters,bool trankChanges)
        {
            var books = await FindAll(trankChanges)
                .FilterBooks(bookParameters.MinPrice,bookParameters.MaxPrice)
                .Search(bookParameters.SearchTerm)
                .Sort(bookParameters.OrderBy).ToListAsync();
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber,bookParameters.PageSize);
                
        }

        public async Task<Book> GetOneBookByIdAsync(int id, bool trankChanges)
        {
            return await FindByCondition(x => x.Id == id,trankChanges).SingleOrDefaultAsync();

        }

        public void UpdateOneBook(Book book)=>Update(book);
    }
}
