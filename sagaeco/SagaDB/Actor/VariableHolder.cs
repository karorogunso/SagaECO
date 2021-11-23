using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SagaLib;

namespace SagaDB.Actor
{
    /// <summary>
    /// 变量不存在是自动返回默认Null值的变量存储器
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class VariableHolder<K, T> : IDictionary<K, T>
    {
        public IConcurrentDictionary<K, T> content = new IConcurrentDictionary<K, T>();
        T nullValue;

        public VariableHolder(T nullValue)
        {
            this.nullValue = nullValue;
        }

        #region IDictionary<K,T> Members

        public void Add(K key, T value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(K key)
        {
            return content.ContainsKey(key);
        }

        public ICollection<K> Keys
        {
            get { return content.Keys; }
        }

        public bool Remove(K key)
        {
            if (content.ContainsKey(key))
                return content.Remove(key);
            else
                return false;
        }

        public bool TryGetValue(K key, out T value)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> Values
        {
            get { return content.Values; }
        }

        public T this[K key]
        {
            get
            {
                if (content.ContainsKey(key))
                    return content[key];
                else
                {                    
                    return nullValue;
                }
            }
            set
            {
                if (content.ContainsKey(key))
                    content[key] = value;
                else
                    content.Add(key, value);
            }
        }

        #endregion

        #region ICollection<KeyValuePair<K,T>> Members

        public void Add(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<K, T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return content.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<K,T>> Members

        public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        {
            return content.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return content.GetEnumerator();
        }

        #endregion
    }
}
