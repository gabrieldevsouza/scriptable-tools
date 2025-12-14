#if UNITY_EDITOR
using System;
using System.Reflection;
using Events;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GameEvent<>), true)]
    public class GameEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Raise Event (default value)"))
            {
                var t = target.GetType();
                // Find Raise(T) on the concrete event type (declared on base generic)
                MethodInfo raiseMethod = t.GetMethod("Raise", BindingFlags.Instance | BindingFlags.Public, null, new[] { t.BaseType.GetGenericArguments()[0] }, null);
                if (raiseMethod == null)
                {
                    Debug.LogWarning("Could not find Raise(T) method on " + target.name);
                    return;
                }

                // Compute true default(T):
                Type payloadType = t.BaseType.GetGenericArguments()[0];
                object defaultValue = payloadType.IsValueType ? Activator.CreateInstance(payloadType) : null;

                raiseMethod.Invoke(target, new[] { defaultValue });
            }
        }
    }
}
#endif