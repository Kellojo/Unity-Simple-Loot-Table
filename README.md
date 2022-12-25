# Unity-Simple-Loot-Table
A simple loot table for Unity. It's easily customizable and allows you to bring your own item classes and types.

## Using a loot table
1. Create a new loot table scriptable object using the create menu `Create/Kellojo/Loot Table/`
2. Setup your loot table with your loot
3. Integrate the loot table into your codebase

```cs
using System.Collections.Generic;
using UnityEngine;
using SimpleLootTable;

public class OrePile : MonoBehaviour
{
    public List<Transform> OreSlots;
    public GameObjectLootTable LootTable;

    private void Awake() {

        // spawn guaranteed drops
        var drops = LootTable.GetGuaranteedDrops();
        drops.ForEach(drop => Instantiate(drop, transform.position, transform.rotation));

        // spawn optional drops
        var optionalDrops = LootTable.GetOptionalDrops(2);
        optionalDrops.ForEach(drop => Instantiate(drop, transform.position, transform.rotation));
        
    }

}

```

## Using a custom item class/type

1. Create a new CS script with the following content:

```cs
using UnityEditor;
using UnityEngine;
using SimpleLootTable;

[CreateAssetMenu(menuName = "Kellojo/Loot Table/Game Object Loot Table")]
public class GameObjectLootTable : LootTableBase<GameObject> {}

[System.Serializable]
public class GameObjectDropConfig : DropConfig<GameObject> { }
```

2. Replace `GameObject` with your custom type/class
3. Adjust the `menuName` to match your class name
4. Ensure your custom item class is serialized by Unity
5. Create another script in your `Editor` folder which contains the editor for your custom loot table type:

```cs
using UnityEditor;
using UnityEngine;
using Kellojo.SimpleLootTable;
using Kellojo.SimpleLootTable.Editor;

[CustomEditor(typeof(GameObjectLootTable))]
public class GameObjectLootTableEditor : LootTableEditorBase<GameObject> { }
```
6. Replace `GameObject` with your custom type/class
7. Rename the class to more closely match your custom type (i.e. `GameObjectLootTableEditor` -> `ItemLootTableEditor`)
7. Done! Your custom loot table can now be used.