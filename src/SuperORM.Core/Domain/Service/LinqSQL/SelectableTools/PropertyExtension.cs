﻿using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    public static class PropertyExtension
    {
        public static IFieldArgument Count(this object _)
        {
            return new Count();
        }
    }

    public class FieldToBeDefined
    {
        public FieldToBeDefined(IFieldArgument fieldArgument)
        {

        }
    }
}
