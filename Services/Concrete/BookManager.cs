﻿using AutoMapper;
using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Abstract;
using Services.Abstract;
using System.Dynamic;

namespace Services.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<BookDto> _shaper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<BookDto> shaper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _shaper = shaper;
        }

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);
            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges)
        {
            if(!bookParameters.ValidPriceRange)
                throw new PriceOutOfRangeBadRequestException();

            var bookWithMetaData = await _manager.Book.GetAllBooksAsync(bookParameters,trackChanges);
            var bookDtos= _mapper.Map<IEnumerable<BookDto>>(bookWithMetaData);

            var shapedData = _shaper.ShapeData(bookDtos, bookParameters.Fields);
            return (shapedData, bookWithMetaData.MetaData);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<BookDto>(book);

        }

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
            var bookForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return (bookForUpdate, book);
        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.UpdateOneBook(entity);
            await _manager.SaveAsync();
        }

        private async Task<Book> GetOneBookByIdAndCheckExists(int id,bool trackChanges)
        {
            var entity=await _manager.Book.GetOneBookByIdAsync(id,trackChanges);
            if (entity is null)
                throw new BookNotFoundException(id);
            return entity;

        }
    }
}
