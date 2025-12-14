using System.Collections.Generic;
using UnityEngine;

namespace RuntimeSets
{
    /// <summary>
    /// Runtime-only set of items (cleared on enable). Use concrete subclasses with CreateAssetMenu.
    /// List-backed: simple, predictable iteration order, great for dozens to a few hundred items.
    /// </summary>
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        [System.NonSerialized] private readonly List<T> _items = new List<T>();

        /// <summary> Read-only view for iteration. </summary>
        public IReadOnlyList<T> Items => _items;

        /// <summary> Current number of items. </summary>
        public int Count => _items.Count;

        protected virtual void OnEnable()
        {
            // Fresh start each play session/domain reload to avoid stale scene refs.
            _items.Clear();
        }

        /// <summary>
        /// Adds the item if not already present.
        /// Returns true if added, false if it was already in the set.
        /// </summary>
        public bool Add(T item)
        {
            if (_items.Contains(item)) return false;
            _items.Add(item);
            return true;
        }

        /// <summary>
        /// Removes the item if present.
        /// Returns true if removed, false if it wasn't in the set.
        /// </summary>
        public bool Remove(T item) => _items.Remove(item);

        /// <summary> Returns true if the item is present. </summary>
        public bool Contains(T item) => _items.Contains(item);

        /// <summary> Empties the set. </summary>
        public void Clear() => _items.Clear();

        /// <summary> Semantic alias for Clear(); keep if you like the naming. </summary>
        public void Reset() => _items.Clear();
    }
}