using Repositories.Abstract;

namespace Repositories.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _context;
        private readonly Lazy<IBookRepository> _bookRepository;

        public RepositoryManager(ApplicationContext context)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_context));
        }
        //lazy loadingle nesne ancak ve ancak kullanıldığı anda ilgili ifade newlenecek
        public IBookRepository Book => _bookRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
