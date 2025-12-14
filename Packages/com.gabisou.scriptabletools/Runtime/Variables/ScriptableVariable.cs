using System;
using UnityEngine;

namespace Variables
{
    public class ScriptableVariable<T> : ScriptableObject
    {
        [Tooltip("Design-time value used to initialize the runtime value when the asset is enabled (e.g., on play).")]
        [SerializeField] private T initialValue;

        [NonSerialized] private T _value;

        /// <summary> Raised when Value changes (optional for lightweight observation). </summary>
        public event Action<T> OnValueChanged;

        /// <summary> Runtime value. Resets from InitialValue on OnEnable(). </summary>
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        /// <summary> Read-only access to the design-time initial value. </summary>
        public T InitialValue => initialValue;

        protected virtual void OnEnable()
        {
            // Ensure runtime value starts from the design-time InitialValue each time the asset is enabled.
            _value = initialValue;
        }

        /// <summary> Restores the runtime value back to InitialValue. Safe to call at scene reset/checkpoints. </summary>
        public void ResetValue() => Value = initialValue;

#if UNITY_EDITOR
        // If you want changes to InitialValue in the editor to reflect immediately in play when recompiled:
        private void OnValidate()
        {
            // Keep _value synced in edit mode so inspectors preview changes.
            if (!Application.isPlaying)
            {
                _value = initialValue;
            }
        }
#endif
    }
}