using Entities.Models;
using System.Dynamic;

namespace Services.Abstract
{
    public interface IDataShaper<T>
    {
        //dönüştürme ve şekillendirme işlemlerini daha esnek bir şekilde gerçekleştirelebilmesi için kullanıldı
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities,string fieldString);
        ShapedEntity ShapeData(T entity,string fieldString);
    }
}
