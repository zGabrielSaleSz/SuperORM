using SuperORM.Core.Utilities.Reflection;
using System;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    public class EntityBuilder
    {
        public static T Build<T>(Dictionary<string, object> entityInformation, ColumnAssimilator columnAssimilator)
            where T : new()
        {
            T entity = new();
            foreach (var propertyEquivalent in entityInformation)
            {
                ReflectionHandler<T> reflectionHandler = new Utilities.Reflection.ReflectionHandler<T>(entity);
                string propertyName = columnAssimilator.GetRespective(propertyEquivalent.Key);
                if (propertyEquivalent.Value is not DBNull)
                {
                    reflectionHandler.SetPropertyValue(propertyName, propertyEquivalent.Value);
                }
            }
            return entity;
        }
    }
}
