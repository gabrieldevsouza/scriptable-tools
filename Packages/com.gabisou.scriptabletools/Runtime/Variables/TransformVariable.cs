using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(fileName = "SOBJ_TransformVariable_TRFM", menuName = "Scriptable Tools/Variables/Transform")]
    public sealed class TransformVariable : ScriptableVariable<Transform> { }

    [Serializable]
    public sealed class TransformReference : ScriptableVariableReference<Transform> { }
}