using UnityEditor;
using UnityEngine;
using Kellojo.SimpleLootTable;

namespace Kellojo.SimpleLootTable.Editor {
    [CustomEditor(typeof(GameObjectLootTable))]
    public class GameObjectLootTableEditor : LootTableEditorBase<GameObject> { }
}
