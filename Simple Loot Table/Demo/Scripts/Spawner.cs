using UnityEngine;
using Kellojo.SimpleLootTable;


public class Spawner : MonoBehaviour {

    public GameObjectLootTable lootTable;
    public int optionalLootCount = 2;

    void Awake() {
        InvokeRepeating("Drop", 0, 5);
    }

    public void Drop() {
        lootTable.GetGuaranteedDrops().ForEach(SpawnLoot);
        lootTable.GetOptionalDrops(optionalLootCount).ForEach(SpawnLoot);
    }

    void SpawnLoot(GameObject drop) {
        var obj = Instantiate(drop, transform);
        Destroy(obj, 5);
    }
}