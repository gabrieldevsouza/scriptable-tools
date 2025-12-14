using UnityEngine;

namespace RuntimeSets
{
    /// <summary>
    /// Clears specified RuntimeSets at scene start.
    /// Only needed if you disabled "Reload Domain" in Enter Play Mode Options.
    /// </summary>
    public sealed class RuntimeSetBootstrapper : MonoBehaviour
    {
        [SerializeField] private ScriptableObject[] setsToClear; // drag your concrete sets here

        private void Awake()
        {
            if (setsToClear == null) return;

            foreach (var so in setsToClear)
            {
                // Only call Clear() on RuntimeSet<T> types
                var clear = so?.GetType().GetMethod("Clear");
                clear?.Invoke(so, null);
            }
        }
    }
}