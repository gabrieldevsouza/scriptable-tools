using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    /// <summary>
    /// Generic ScriptableObject event channel.
    /// Create concrete assets (e.g., VazioGameEvent) and wire up listeners in scenes.
    /// </summary>
    public class GameEvent<T> : ScriptableObject
    {
        /// <summary> Registered listeners. </summary>
        private readonly List<GameEventListener<T>> _listeners = new List<GameEventListener<T>>();

        /// <summary>
        /// Raises the event with a payload. Iterates in reverse order so listeners
        /// can safely unregister during dispatch without affecting iteration.
        /// </summary>
        public void Raise(T value)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value);
            }
        }

        /// <summary> Registers a listener if it is not already present. </summary>
        public void RegisterListener(GameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        /// <summary> Unregisters a listener if present (defensive). </summary>
        public void UnregisterListener(GameEventListener<T> listener)
        {
            int idx = _listeners.IndexOf(listener);
            if (idx >= 0) _listeners.RemoveAt(idx);
        }

#if UNITY_EDITOR
        /// <summary> Editor-only convenience to raise with default(T) from the asset context menu. </summary>
        [ContextMenu("Raise (Editor Debug: default(T))")]
        private void RaiseDefaultInEditor() => Raise(default);
#endif
    }
}