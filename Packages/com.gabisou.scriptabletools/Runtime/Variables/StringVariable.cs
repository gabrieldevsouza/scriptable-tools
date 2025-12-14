using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(fileName = "SOBJ_StringVariable_STRING", menuName = "Scriptable Tools/Variables/String")]
    public sealed class StringVariable : ScriptableVariable<string> { }

    [Serializable]
    public sealed class StringReference : ScriptableVariableReference<string> { }
}