using UnityEngine;

namespace RuntimeSets
{
    [CreateAssetMenu(fileName = "SOBJ_TransformRuntimeSet", menuName = "Scriptable Tools/Runtime Sets/Transform Set")]
    public sealed class TransformRuntimeSet : RuntimeSet<Transform> { }
}