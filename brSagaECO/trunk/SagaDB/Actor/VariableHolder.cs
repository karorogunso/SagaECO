using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SagaDB.Actor
{
    /// <summary>
    /// 变量不存在是自动返回默认Null值的变量存储器
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class VariableHolder<K, T> : System.Collections.Generic.IDictionary<K, T>
    {
        public Dictionary<K, T> content = new Dictionary<K, T>();
        T nullValue;

        public VariableHolder(T nullValue)
        {
            this.nullValue = nullValue;
        }

        #region IDictionary<K,T> Members

        public void Add(K key, T value)
        {
            //throw new NotImplementedException();
            if (content.ContainsKey(key))
                content[key] = value;
            else
                content.Add(key, value);
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
            //throw new NotImplementedException();
            return content.TryGetValue(key, out value);
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
            //throw new NotImplementedException();
            content.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            //throw new NotImplementedException();
            content.Clear();
        }

        public bool Contains(KeyValuePair<K, T> item)
        {
            //throw new NotImplementedException();
            return content.ContainsKey(item.Key);
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
            //throw new NotImplementedException();
            if (content.ContainsKey(item.Key))
                return content.Remove(item.Key);
            else
                return false;
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
