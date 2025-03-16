using UnityEditor;
using UnityEngine;

namespace RocinanteGames.Editor
{
    [InitializeOnLoad]
    public static class HierarchyWindowGroupHeader
    {
        static readonly GUIStyle _style;
        static Color baseColor = Color.gray;
    
        static HierarchyWindowGroupHeader()
        {
            _style = new GUIStyle();
            UpdateStyle();
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }
    
        static void UpdateStyle()
        {
            _style.fontSize = 14;
            _style.fontStyle = FontStyle.Bold;
            _style.alignment = TextAnchor.MiddleRight;
            _style.normal.textColor = Color.black;;
            EditorApplication.RepaintHierarchyWindow();
        }

        static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (gameObject != null && gameObject.name.StartsWith("---", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, baseColor);
                EditorGUI.LabelField(selectionRect, gameObject.name.Replace("-", "").ToUpperInvariant(),_style);
            }
        }
    }
}
