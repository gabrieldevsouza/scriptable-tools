using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(fileName = "SOBJ_Vector3Variable_VEC3", menuName = "Scriptable Tools/Variables/Vector3")]
    public sealed class Vector3Variable : ScriptableVariable<Vector3> { }

    [Serializable]
    public sealed class Vector3Reference : ScriptableVariableReference<Vector3> { }
}