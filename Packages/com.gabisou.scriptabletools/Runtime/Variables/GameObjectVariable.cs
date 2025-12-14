using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(fileName = "SOBJ_GameObjectVariable_GOBJ", menuName = "Scriptable Tools/Variables/GameObject")]
    public sealed class GameObjectVariable : ScriptableVariable<GameObject> { }

    [Serializable]
    public sealed class GameObjectReference : ScriptableVariableReference<GameObject> { }
}