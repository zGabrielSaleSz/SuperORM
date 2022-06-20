using System;
using System.Collections;

namespace SuperORM.Core.Utilities
{
    public class TypeQualifier
    {
        private readonly object _variable;
        public TypeQualifier(object variable)
        {
            _variable = variable;
        }

        public bool IsBoolean()
        {
            return IsTypeOf<bool>();
        }

        public bool IsByte()
        {
            return IsTypeOf<byte>();
        }

        public bool IsString()
        {
            return IsTypeOf<string>();
        }

        public bool IsSByte()
        {
            return IsTypeOf<sbyte>();
        }

        public bool IsTypeOf<T>()
        {
            return _variable.GetType().Equals(typeof(T));
        }
        public bool IsNull()
        {
            return _variable == null;
        }
        public bool IsEnumerable()
        {
            if (IsNull())
            {
                return false;
            }
            return typeof(IEnumerable).IsAssignableFrom(GetVariableType()) && !IsTypeOf<string>();
        }
        public IEnumerable GetAsEnumerable()
        {
            return _variable as IEnumerable;
        }
        public Type GetVariableType()
        {
            return _variable.GetType();
        }
        public T GetAs<T>()
        {
            return (T)Convert.ChangeType(_variable, typeof(T));
        }
    }
}
