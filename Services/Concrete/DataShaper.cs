using Entities.Models;
using Services.Abstract;
using System.Dynamic;
using System.Reflection;

namespace Services.Concrete
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }
        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldString)
        {
            var requiredFileds = GetRequiredProperties(fieldString);
            return FetchData(entities, requiredFileds);
        }

        public ShapedEntity ShapeData(T entity, string fieldString)
        {
            var requiredProperties = GetRequiredProperties(fieldString);
            return FetchDataForEntity(entity, requiredProperties);
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsStirng)
        {
            var requiredFiels=new List<PropertyInfo>();
            if (!string.IsNullOrEmpty(fieldsStirng))
            {
                var fields = fieldsStirng.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    if (property is null)
                        continue;
                    requiredFiels.Add(property);
                }

            }
            else
            {
                requiredFiels=Properties.ToList();
            }
            return requiredFiels;
        }

        private ShapedEntity FetchDataForEntity(T entity,IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject=new ShapedEntity();
            foreach (var property in requiredProperties)
            {
                var objectPropertyValue=property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
            }

            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (int)objectProperty.GetValue(entity);

            return shapedObject;
        }

        private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entites,IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData=new List<ShapedEntity>();
            foreach (var entity in entites)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}
