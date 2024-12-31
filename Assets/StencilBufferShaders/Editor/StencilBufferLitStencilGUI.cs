using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEditor.Rendering.Universal.ShaderGUI
{
    internal class StencilBufferLitStencilGUI
    {

        static string[] comparisonValues = new string[] { "Greater", "GEqual", "Less", "LEqual", "Equal", "NotEqual", "Always", "Never" };
        static string[] stencilValues = new string[] { "Keep", "Zero", "Replace", "IncrSat", "DecrSat", "Invert", "IncrWrap", "DecrWrap" };

        internal static class Styles
        {
            public static readonly GUIContent stencilBufferInputs = EditorGUIUtility.TrTextContent("Stencil Buffer Inputs", "These settings define the use of the stencil buffer of the material.");
            public static readonly GUIContent stencilIDText = EditorGUIUtility.TrTextContent("Stencil ID", "The ID for the stencil buffer [0-255].");
            public static readonly GUIContent compText = EditorGUIUtility.TrTextContent("Comparation Op.", "Stencil buffer comparation operation.");
            public static readonly GUIContent passOperationText = EditorGUIUtility.TrTextContent("Pass Op.", "Stencil buffer pass operation.");
        }

        public struct LitProperties
        {
            public MaterialProperty stencilID;
            public MaterialProperty compMode;
            public MaterialProperty passOperation;

            public LitProperties(MaterialProperty[] properties)
            {
                stencilID = BaseShaderGUI.FindProperty("_StencilID", properties, false);
                compMode = BaseShaderGUI.FindProperty("_CompMode", properties, false);
                passOperation = BaseShaderGUI.FindProperty("_Action", properties, false);
            }
        }

        public static void DoDetailArea(LitProperties properties, MaterialEditor materialEditor)
        {
            materialEditor.IntSliderShaderProperty(properties.stencilID, Styles.stencilIDText);
            materialEditor.PopupShaderProperty(properties.compMode, Styles.compText, comparisonValues);
            materialEditor.PopupShaderProperty(properties.passOperation, Styles.passOperationText, stencilValues);
        }

        public static void SetMaterialKeywords(Material material)
        {
            if (material.HasProperty("_StencilID"))
            {
                bool hasStencilID = material.GetFloat("_StencilID") > 0f;
                CoreUtils.SetKeyword(material, "_STENCIL_BUFFER", hasStencilID);
            }
        }
    }
}
