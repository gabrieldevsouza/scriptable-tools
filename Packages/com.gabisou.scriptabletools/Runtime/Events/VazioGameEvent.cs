using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "SOBJ_VazioGameEvent", menuName = "Scriptable Tools/Events/Vazio (Void)")]
    public sealed class VazioGameEvent : GameEvent<Vazio>
    {
#if UNITY_EDITOR
        [ContextMenu("Raise (Editor Debug)")]
        private void RaiseDebug() => Raise(default);
#endif

        /// <summary>
        /// Raises the event with an empty Vazio payload.
        /// </summary>
        public void Raise() => Raise(default);
    }
}