using BookStoreApi.Models;
using BookStoreApi.Repositories.Config;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options):base(options)
        { 

        }
       
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
