using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu (fileName = "SOBJ_IntVariable_INT", menuName = "Scriptable Tools/Variables/Int")]
    public sealed class IntVariable : ScriptableVariable<int> { }
    
    [Serializable]
    public sealed class IntReference : ScriptableVariableReference<int> { }
}