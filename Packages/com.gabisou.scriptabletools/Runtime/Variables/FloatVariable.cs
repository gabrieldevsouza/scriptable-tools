using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu (fileName = "SOBJ_FloatVariable_FLOAT", menuName = "Scriptable Tools/Variables/Float")]
    public sealed class FloatVariable : ScriptableVariable<float> { }
    
    [Serializable]
    public sealed class FloatReference : ScriptableVariableReference<float> { }
}