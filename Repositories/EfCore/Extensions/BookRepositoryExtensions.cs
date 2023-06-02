using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repositories.EfCore.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice)
        {
            return books.Where(book => book.Price >= minPrice && book.Price <= maxPrice);
        }

        public static IQueryable<Book> Search(this IQueryable<Book> books ,string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return books;

            var loweCaseTerm=searchTerm.Trim().ToLower();
            return books.Where(b=>b.Title.ToLower().Contains(loweCaseTerm));
        }

        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {
            if(string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(x=>x.Id);

            var orderParams=orderByQueryString.Trim().Split(',');

            var propertyInfos=typeof(Book).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertFromQueryName = param.Split(' ')[0];

                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty is null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");

               
            }

            var orderQuery=orderQueryBuilder.ToString().TrimEnd(',',' ');

            if(orderQuery is null)
                return books.OrderBy(x=>x.Id);

            return books.OrderBy(orderQuery);
        }
    }
}
