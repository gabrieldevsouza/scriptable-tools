#if UNITY_EDITOR
using RuntimeSets;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    // Shows a live, read-only view for any concrete RuntimeSet<T> asset.
    // NOTE: This attribute works in most projects. If your compiler ever complains
    // about open generics here, see the fallback version below.
    [CustomEditor(typeof(RuntimeSet<>), true)]
    public class RuntimeSetViewerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var t = target.GetType();
            var itemsProp = t.GetProperty("Items");
            if (itemsProp == null) return;

            var items = itemsProp.GetValue(target) as System.Collections.IEnumerable;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime Items (read-only)", EditorStyles.boldLabel);

            int count = 0;
            if (items != null)
            {
                foreach (var it in items)
                {
                    // Show UnityEngine.Object as object fields; show plain values as labels.
                    if (it is Object uObj)
                    {
                        EditorGUILayout.ObjectField($"[{count}]", uObj, typeof(Object), true);
                    }
                    else
                    {
                        EditorGUILayout.LabelField($"[{count}]", it?.ToString() ?? "null");
                    }
                    count++;
                }
            }

            EditorGUILayout.LabelField("Count", count.ToString());

            if (Application.isPlaying)
            {
                EditorGUILayout.Space();
                if (GUILayout.Button("Clear (Play Mode)"))
                {
                    var clearMethod = t.GetMethod("Clear");
                    clearMethod?.Invoke(target, null);
                    EditorUtility.SetDirty((Object)target);
                }
            }
        }
    }
}
#endif