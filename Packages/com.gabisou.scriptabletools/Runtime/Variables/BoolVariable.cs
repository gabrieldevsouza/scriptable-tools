using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu (fileName = "SOBJ_BoolVariable_BOOL", menuName = "Scriptable Tools/Variables/Bool")]
    public sealed class BoolVariable : ScriptableVariable<bool> { }
    [Serializable]
    public sealed class BoolReference : ScriptableVariableReference<bool> { }
}