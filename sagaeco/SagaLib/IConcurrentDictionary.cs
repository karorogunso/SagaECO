using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace SagaLib
{
    public class IConcurrentDictionary<K, T> : IDictionary<K, T>
    {
        public ConcurrentDictionary<K, T> content;
        T nullValue;

        public IConcurrentDictionary()
        {
            content = new ConcurrentDictionary<K, T>();
        }
        #region IDictionary<K,T> Members

        public void Add(K key, T value)
        {
            content[key] = value;
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
            return content.TryRemove(key, out T Tvalue);
        }

        public bool TryGetValue(K key, out T value)
        {
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
                if (content.TryGetValue(key, out T value))
                    return value;
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                content[key] = value;
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
            content.Clear();
        }

        public bool Contains(KeyValuePair<K, T> item)
        {
            return content.Contains(item);
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
