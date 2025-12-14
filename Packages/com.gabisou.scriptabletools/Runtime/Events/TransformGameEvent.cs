using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "SOBJ_TransformGameEvent", menuName = "Scriptable Tools/Events/Transform")]
    public sealed class TransformGameEvent : GameEvent<Transform> { }
}