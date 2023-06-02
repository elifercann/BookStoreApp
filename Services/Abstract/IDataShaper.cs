using System.Dynamic;

namespace Services.Abstract
{
    public interface IDataShaper<T>
    {
        //dönüştürme ve şekillendirme işlemlerini daha esnek bir şekilde gerçekleştirelebilmesi için kullanıldı
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities,string fieldString);
        ExpandoObject ShapeData(T entity,string fieldString);
    }
}
