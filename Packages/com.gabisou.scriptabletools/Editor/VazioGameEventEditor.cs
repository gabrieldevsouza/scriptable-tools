#if UNITY_EDITOR
using Events;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(VazioGameEvent))]
    public sealed class VazioGameEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Raise Event"))
            {
                var evt = (VazioGameEvent)target;
                evt.Raise();
                // Optional: mark dirty so changes (if any) are recorded in editor
                EditorUtility.SetDirty(evt);
            }
        }
    }
}
#endif