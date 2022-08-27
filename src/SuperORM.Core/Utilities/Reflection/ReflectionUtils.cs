using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SuperORM.Core.Utilities.Reflection
{
    public class ReflectionUtils
    {
        public static void SetPropertyValue(object theObject, string propertyName, object value)
        {
            Type type = theObject.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            MethodInfo setter = property.SetMethod;
            setter.Invoke(theObject, new object[] { value });
        }

        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static IEnumerable<PropertyInfo> GetProperties(object src)
        {
            return src.GetType().GetProperties();
        }

        public static IEnumerable<string> GetObjectProperties(object src)
        {
            return GetProperties(src).Select(x => x.Name);
        }

        public static IEnumerable<string> GetObjectProperties<T>()
        {
            return typeof(T).GetProperties().Select(x => x.Name);
        }

        public static Type GetPropertyType(object src, string propertyName)
        {
            return src.GetType().GetProperties().Where(x => x.Name == propertyName).Select(x => x.PropertyType).First();
        }

        public static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public static T GetAsType<T>(object value)
        {
            if(value == null)
                return default(T);

            Type type = typeof(T);
            if (IsNullableType(type))
            {
                type = Nullable.GetUnderlyingType(type);
                return (T)Convert.ChangeType(value, type);
            }
            return (T)Convert.ChangeType(value, type);
        }

        public static MethodInfo BuildEnumerableGenericMethod<GenericMethodType>(string methodName, int parametersAmount)
        {
            return typeof(Enumerable).GetMethods()
                .Where(x => x.Name == methodName)
                .Single(x => x.GetParameters().Length == parametersAmount)
                .MakeGenericMethod(typeof(GenericMethodType));
        }
    }
}
