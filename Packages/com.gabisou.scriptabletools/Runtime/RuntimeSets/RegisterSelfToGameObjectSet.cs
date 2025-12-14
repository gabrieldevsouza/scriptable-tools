using UnityEngine;

namespace RuntimeSets
{
    /// <summary>
    /// Registers THIS GameObject into a GameObjectRuntimeSet on enable, and removes on disable.
    /// Attach to any object you want tracked (e.g., hidden objects).
    /// </summary>
    [AddComponentMenu("Scriptable Tools/Runtime Sets/Register Self To GameObject Set")]
    public sealed class RegisterSelfToGameObjectSet : MonoBehaviour
    {
        [SerializeField] private GameObjectRuntimeSet runtimeSet;

        private void OnEnable()
        {
            if (runtimeSet == null)
            {
                Debug.LogWarning($"[{name}] RegisterSelfToGameObjectSet has no RuntimeSet assigned.", this);
                return;
            }
            runtimeSet.Add(gameObject);
        }

        private void OnDisable()
        {
            if (runtimeSet == null) return;
            runtimeSet.Remove(gameObject);
        }
    }
}