using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "SOBJ_StringGameEvent", menuName = "Scriptable Tools/Events/String")]
    public sealed class StringGameEvent : GameEvent<string> { }
}