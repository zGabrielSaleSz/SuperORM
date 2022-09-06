﻿using System;
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
            Type type = GetActualType(property);
            property.SetMethod.Invoke(_src, new object[] { Convert.ChangeType(value, type) });
        }

        internal bool IsEntity(string propertyName)
        {
            PropertyInfo property = _type.GetProperty(propertyName);
            if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                return false;
            return true;
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

        internal Type GetActualType(PropertyInfo property)
        {
            Type propertyType = property.PropertyType;
            if (ReflectionUtils.IsNullableType(propertyType))
            {
                propertyType = Nullable.GetUnderlyingType(propertyType);
            }
            return propertyType;
        }

        public IEnumerable<string> GetPropertiesName()
        {
            return _type.GetProperties().Select(p => p.Name);
        }

        public IEnumerable<PropertyInfo> GetColumnProperties()
        {
            return ReflectionUtils.GetColumnProperties(_type);
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
