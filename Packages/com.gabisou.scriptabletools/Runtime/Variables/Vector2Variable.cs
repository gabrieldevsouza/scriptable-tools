using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(fileName = "SOBJ_Vector2Variable_VEC2", menuName = "Scriptable Tools/Variables/Vector2")]
    public sealed class Vector2Variable : ScriptableVariable<Vector2> { }

    [Serializable]
    public sealed class Vector2Reference : ScriptableVariableReference<Vector2> { }

}