using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    /// <summary>
    /// Key不存在时自动创建新实例的变量存储器
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class VariableHolderA<K, T> : System.Collections.Generic.IDictionary<K, T> where T :new ()
    {
        public Dictionary<K, T> content = new Dictionary<K, T>();
        
        public VariableHolderA()
        {
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
                    T newf = new T();
                    content.Add(key, newf);
                    return newf;
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
