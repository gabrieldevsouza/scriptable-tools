using UnityEngine;

namespace RuntimeSets
{
    /// <summary>
    /// Registers THIS Transform into a TransformRuntimeSet on enable, and removes on disable.
    /// </summary>
    [AddComponentMenu("Scriptable Tools/Runtime Sets/Register Self To Transform Set")]
    public sealed class RegisterSelfToTransformSet : MonoBehaviour
    {
        [SerializeField] private TransformRuntimeSet runtimeSet;

        private void OnEnable()
        {
            if (!runtimeSet)
            {
                Debug.LogWarning($"[{name}] RegisterSelfToTransformSet has no RuntimeSet assigned.", this);
                return;
            }
            runtimeSet.Add(transform);
        }

        private void OnDisable()
        {
            if (runtimeSet == null) return;
            runtimeSet.Remove(transform);
        }
    }
}