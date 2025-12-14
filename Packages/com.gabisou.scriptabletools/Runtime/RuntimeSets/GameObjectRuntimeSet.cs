
using UnityEngine;

namespace RuntimeSets
{
    [CreateAssetMenu(fileName = "SOBJ_GameObjectRuntimeSet", menuName = "Scriptable Tools/Runtime Sets/GameObject Set")]
    public sealed class GameObjectRuntimeSet : RuntimeSet<GameObject> { }
}