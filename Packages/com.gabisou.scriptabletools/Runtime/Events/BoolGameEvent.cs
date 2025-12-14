using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "SOBJ_BoolGameEvent", menuName = "Scriptable Tools/Events/Bool")]
    public sealed class BoolGameEvent : GameEvent<bool> { }
}