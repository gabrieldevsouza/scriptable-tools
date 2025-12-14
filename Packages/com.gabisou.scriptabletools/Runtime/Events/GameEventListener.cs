using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class GameEventListener<T> : MonoBehaviour
    {
        [SerializeField, Tooltip("Event asset to subscribe to when this component is enabled.")]
        private GameEvent<T> gameEvent;

        [SerializeField, Tooltip("UnityEvent invoked when the event is raised. Add methods to respond here.")]
        private UnityEvent<T> response;


        private void OnEnable()
        {
            if (gameEvent == null)
            {
                Debug.LogWarning($"[{name}] GameEventListener<{typeof(T).Name}> has no Event assigned.", this);
                return;
            }
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) return;
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T value)
        {
            response?.Invoke(value);
        }
    }
}