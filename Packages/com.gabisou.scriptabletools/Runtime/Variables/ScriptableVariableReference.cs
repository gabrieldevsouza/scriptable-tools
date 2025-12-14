using System;
using UnityEngine;

namespace Variables
{
    [Serializable]
    public class ScriptableVariableReference<T>
    {
        [Tooltip("If true, uses the Constant Value below. If false, uses the Variable asset.")]
        [SerializeField] private bool useConstant = true;

        [Tooltip("Literal value used when 'Use Constant' is enabled or when no Variable asset is assigned.")]
        [SerializeField] private T constantValue;

        [Tooltip("ScriptableObject variable asset to read/write when 'Use Constant' is disabled.")]
        [SerializeField] private ScriptableVariable<T> variable;

        /// <summary>
        /// Effective value. Reads/writes the ScriptableVariable when available and UseConstant is false; otherwise uses the constant.
        /// </summary>
        public T Value
        {
            get
            {
                if (useConstant || variable == null) return constantValue;
                return variable.Value;
            }
            set
            {
                if (useConstant || variable == null) constantValue = value;
                else variable.Value = value;
            }
        }

        /// <summary> Inspector toggle to switch between constant and variable modes. </summary>
        public bool UseConstant
        {
            get => useConstant;
            set => useConstant = value;
        }

        /// <summary> Backing ScriptableVariable asset used when not in constant mode. </summary>
        public ScriptableVariable<T> Variable
        {
            get => variable;
            set => variable = value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // If a variable asset is assigned, prefer using it.
            if (variable != null && useConstant)
                useConstant = false;

            // If the variable reference was cleared, fall back to constant mode.
            if (variable == null && !useConstant)
                useConstant = true;
        }
#endif
    }
}
