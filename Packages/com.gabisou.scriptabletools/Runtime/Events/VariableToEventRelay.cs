using UnityEngine;
using Variables;

namespace Events
{
    public class VariableToEventRelay<T> : MonoBehaviour
    {
        [SerializeField] private ScriptableVariable<T> variable;
        [SerializeField] private GameEvent<T> gameEvent;

        [SerializeField, Tooltip("Raise once on enable with the current runtime value.")]
        private bool raiseOnEnableWithCurrent = false;

        private void OnEnable()
        {
            if (variable == null || gameEvent == null)
            {
                Debug.LogWarning($"[{name}] VariableToEventRelay<{typeof(T).Name}> is missing references.", this);
                return;
            }

            variable.OnValueChanged += HandleValueChanged;

            if (raiseOnEnableWithCurrent)
                gameEvent.Raise(variable.Value);
        }

        private void OnDisable()
        {
            if (variable == null) return;
            variable.OnValueChanged -= HandleValueChanged;
        }

        private void HandleValueChanged(T newValue) => gameEvent.Raise(newValue);
    }
}