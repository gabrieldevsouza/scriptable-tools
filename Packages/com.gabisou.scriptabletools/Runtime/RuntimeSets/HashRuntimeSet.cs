using System.Collections.Generic;
using UnityEngine;

namespace RuntimeSets
{
    public abstract class HashRuntimeSet<T> : ScriptableObject
    {
        [System.NonSerialized] private readonly HashSet<T> items = new HashSet<T>();

        public IReadOnlyCollection<T> Items => items;
        public int Count => items.Count;

        protected virtual void OnEnable() => items.Clear();

        public bool Add(T item) => items.Add(item);
        public bool Remove(T item) => items.Remove(item);
        public bool Contains(T item) => items.Contains(item);
        public void Clear() => items.Clear();
        public void Reset() => items.Clear();
    }
}