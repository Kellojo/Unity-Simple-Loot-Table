using UnityEngine;
using UnityEditor;
using Kellojo.SimpleLootTable;

namespace Kellojo.SimpleLootTable.Editor {

    [CustomPropertyDrawer(typeof(DropConfig<>))]
    public class DropConfigPropertyDrawer : DropConfigPropertyDrawerBase {

        const int FIELD_SPACING = 8;
        const int LINE_SPACING = 8;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var overallWidth = position.width;
            var initialX = position.x;


            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            position.height = (position.height - LINE_SPACING) / 2;

            position = DrawLabel(position, overallWidth, "Drop", 0.7f);
            position = DrawLabel(position, overallWidth, "Min Count", 0.15f);
            position = DrawLabel(position, overallWidth, "Max Count", 0.15f);

            position.y += GetPropertyHeight(property, label) / 2;
            position.x = initialX;

            position = DrawPropertyField(position, overallWidth, property.FindPropertyRelative("Drop"), 0.7f);
            position = DrawPropertyField(position, overallWidth, property.FindPropertyRelative("MinCount"), 0.15f);
            position = DrawPropertyField(position, overallWidth, property.FindPropertyRelative("MaxCount"), 0.15f);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

    }

}
