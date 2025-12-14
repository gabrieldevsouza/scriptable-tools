using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "SOBJ_GameObjectGameEvent", menuName = "Scriptable Tools/Events/GameObject")]
    public sealed class GameObjectGameEvent : GameEvent<GameObject> { }
}