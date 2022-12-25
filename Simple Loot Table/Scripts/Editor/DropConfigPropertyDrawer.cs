using UnityEngine;
using UnityEditor;
using Kellojo.SimpleLootTable;

namespace Kellojo.SimpleLootTable.Editor {

    [CustomPropertyDrawer(typeof(DropConfig<>))]
    public class DropConfigPropertyDrawer : PropertyDrawer {

        const int FIELD_SPACING = 8;
        const int LINE_SPACING = 8;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var overallWidth = position.width;
            var initialX = position.x;


            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            position.height = (position.height - LINE_SPACING) / 2;

            position = DrawLabel(position, overallWidth, "Weight", 0.15f);
            position = DrawLabel(position, overallWidth, "Drop", 0.55f);
            position = DrawLabel(position, overallWidth, "Min Count", 0.15f);
            position = DrawLabel(position, overallWidth, "Max Count", 0.15f);

            position.y += GetPropertyHeight(property, label) / 2;
            position.x = initialX;

            position = DrawPropertyField(position, overallWidth, property.FindPropertyRelative("Weight"), 0.15f);
            position = DrawPropertyField(position, overallWidth, property.FindPropertyRelative("Drop"), 0.55f);
            position = DrawPropertyField(position, overallWidth, property.FindPropertyRelative("MinCount"), 0.15f);
            position = DrawPropertyField(position, overallWidth, property.FindPropertyRelative("MaxCount"), 0.15f);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return base.GetPropertyHeight(property, label) * 2 + LINE_SPACING;
        }

        Rect DrawPropertyField(Rect position, float overallWidth, SerializedProperty property, float widthPercentage) {
            float width = widthPercentage * overallWidth;
            position.width = width - FIELD_SPACING;
            EditorGUI.PropertyField(position, property, GUIContent.none);

            position.x += width;
            return position;
        }

        Rect DrawLabel(Rect position, float overallWidth, string label, float widthPercentage) {
            float width = widthPercentage * overallWidth;
            position.width = width - FIELD_SPACING;
            EditorGUI.LabelField(position, label);

            position.x += width;
            return position;
        }


    }

}
