using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EfCore;

namespace BookStoreApi.ContextFactory
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            //configuration
            var configuration=new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            //DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                prj=>prj.MigrationsAssembly("BookStoreApi"));

            return new ApplicationContext(builder.Options);
               
        }
    }
}
