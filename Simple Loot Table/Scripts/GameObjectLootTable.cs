using UnityEditor;
using UnityEngine;
using Kellojo.SimpleLootTable;
using Kellojo.SimpleLootTable.Editor;


namespace Kellojo.SimpleLootTable {

    [CreateAssetMenu(menuName = "Kellojo/Loot Table/Game Object Loot Table")]
    public class GameObjectLootTable : LootTableBase<GameObject> {}

    [System.Serializable]
    public class GameObjectDropConfig : DropConfig<GameObject> { }

}

#if UNITY_EDITOR

[CustomEditor(typeof(GameObjectLootTable))]
public class GameObjectLootTableEditor : LootTableEditorBase<GameObject> { }

#endif