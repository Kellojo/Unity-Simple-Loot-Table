using UnityEngine;
using UnityEditor;


namespace Kellojo.SimpleLootTable.Editor {


    public abstract class DropConfigPropertyDrawerBase : PropertyDrawer {

        const int FIELD_SPACING = 8;
        const int LINE_SPACING = 8;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return base.GetPropertyHeight(property, label) * 2 + LINE_SPACING;
        }

        protected Rect DrawPropertyField(Rect position, float overallWidth, SerializedProperty property, float widthPercentage) {
            float width = widthPercentage * overallWidth;
            position.width = width - FIELD_SPACING;
            EditorGUI.PropertyField(position, property, GUIContent.none);

            position.x += width;
            return position;
        }

        protected Rect DrawLabel(Rect position, float overallWidth, string label, float widthPercentage) {
            float width = widthPercentage * overallWidth;
            position.width = width - FIELD_SPACING;
            EditorGUI.LabelField(position, label);

            position.x += width;
            return position;
        }


    }

}
