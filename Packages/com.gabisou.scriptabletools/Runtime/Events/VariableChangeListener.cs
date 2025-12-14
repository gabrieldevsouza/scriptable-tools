using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Events
{
    public class VariableChangeListener<T> : MonoBehaviour
    {
        [SerializeField] private ScriptableVariable<T> variable;

        [SerializeField] private UnityEvent<T> onValueChanged;

        [SerializeField, Tooltip("Invoke once on enable with the current runtime value.")]
        private bool invokeOnEnableWithCurrent = true;

        private void OnEnable()
        {
            if (variable == null)
            {
                Debug.LogWarning($"[{name}] VariableChangeListener<{typeof(T).Name}> has no variable assigned.", this);
                return;
            }

            variable.OnValueChanged += HandleValueChanged;

            if (invokeOnEnableWithCurrent)
                HandleValueChanged(variable.Value);
        }

        private void OnDisable()
        {
            if (variable == null) return;
            variable.OnValueChanged -= HandleValueChanged;
        }

        private void HandleValueChanged(T newValue) => onValueChanged?.Invoke(newValue);
    }
}