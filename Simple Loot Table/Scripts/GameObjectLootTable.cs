using UnityEditor;
using UnityEngine;
using Kellojo.SimpleLootTable;

namespace Kellojo.SimpleLootTable {

    [CreateAssetMenu(menuName = "Kellojo/Loot Table/Game Object Loot Table")]
    public class GameObjectLootTable : LootTableBase<GameObject> {}

    [System.Serializable]
    public class GameObjectDropConfig : DropConfig<GameObject> { }

}
