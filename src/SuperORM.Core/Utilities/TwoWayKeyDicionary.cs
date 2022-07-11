using System.Collections.Generic;

namespace SuperORM.Core.Utilities
{
    public class TwoWayKeyDicionary<T>
    {
        private readonly Dictionary<T, T> _dicionary;
        private readonly Dictionary<T, T> _dicionaryTwo;
        public TwoWayKeyDicionary()
        {
            _dicionary = new Dictionary<T, T>();
            _dicionaryTwo = new Dictionary<T, T>();
        }

        public TwoWayKeyDicionary(Dictionary<T, T> values)
        {
            AddRange(values);
        }

        public void AddRange(Dictionary<T, T> values)
        {
            foreach (var value in values)
            {
                Add(value.Key, value.Value);
            }
        }
        public void Add(T a, T b)
        {
            _dicionary[a] = b;
            _dicionaryTwo[b] = a;
        }

        public T GetValueFromRightAsKey(T key)
        {
            if (_dicionaryTwo.ContainsKey(key))
                return _dicionaryTwo[key];
            return key;
        }

        public T GetValueFromLeftKey(T key)
        {
            if (_dicionary.ContainsKey(key))
                return _dicionary[key];
            return key;
        }

        public T GetRespective(T key)
        {
            if (_dicionary.ContainsKey(key))
                return _dicionary[key];
            else if (_dicionaryTwo.ContainsKey(key))
                return _dicionaryTwo[key];
            else
                return key;
        }
    }
}
