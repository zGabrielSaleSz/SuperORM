using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SuperORM.Core.Utilities.Reflection
{
    public class ReflectionHandler<T>
    {
        private readonly Type _type;
        private readonly T _src;
        public ReflectionHandler(T source)
        {
            _type = typeof(T);
            _src = source;
        }
        public string GetTypeName()
        {
            return _type.Name;
        }

        public object GetPropertyValue(string propertyName)
        {
            return GetPropertyValue<object>(_src, propertyName);
        }

        public void SetPropertyValue(string propertyName, object value)
        {
            PropertyInfo property = _type.GetProperty(propertyName);
            property.SetMethod.Invoke(_src, new object[] { Convert.ChangeType(value, property.PropertyType) });
        }

        public bool IsNullableProperty(string propertyName)
        {
            PropertyInfo property = _type.GetProperty(propertyName);
            if (property == null)
                return true;

            Type propertyType = property.PropertyType;
            if (!propertyType.IsValueType) 
                return true;

            return Nullable.GetUnderlyingType(property.PropertyType) != null;
        }

        public IEnumerable<string> GetPropertiesName()
        {
            return _type.GetProperties().Select(p => p.Name);
        }

        public static TResult GetPropertyValue<TResult>(T entity, string propertyName)
        {
            return (TResult)entity.GetType().GetProperty(propertyName).GetValue(entity, null);
        }

        public static IEnumerable<TResult> GetValuesByProperty<TResult>(T[] entities, string propertyName)
        {
            foreach (T entity in entities)
            {
                yield return GetPropertyValue<TResult>(entity, propertyName);
            }
        }
    }
}
