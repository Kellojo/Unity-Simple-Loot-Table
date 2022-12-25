using UnityEngine;
using UnityEditor;
using Kellojo.SimpleLootTable;

namespace Kellojo.SimpleLootTable.Editor {

    [CustomEditor(typeof(LootTableBase<>), true)]
    public class LootTableFallbackEditor : UnityEditor.Editor {


        public override void OnInspectorGUI() {

            EditorGUILayout.Space(32);
            EditorGUILayout.LabelField("Loot Table", GetTitleStyle());
            EditorGUILayout.Space(32);

            EditorGUILayout.HelpBox("Your custom type isn't properly setup. Please follow the guide linked below to setup the editor.", MessageType.Warning);
            if (EditorGUILayout.LinkButton("Setup Guide")) {
                Application.OpenURL("https://github.com/Kellojo/Unity-Simple-Loot-Table/blob/main/README.md#using-a-custom-item-classtype");
            }

        }


        GUIStyle GetTitleStyle() {
            var style = new GUIStyle(EditorStyles.label);
            style.fontSize = 20;
            style.alignment = TextAnchor.MiddleCenter;

            return style;
        }


    }

}