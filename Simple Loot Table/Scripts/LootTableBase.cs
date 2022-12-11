using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kellojo.SimpleLootTable {

    public abstract class LootTableBase<T> : ScriptableObject where T : Object {


        [SerializeField] public List<DropConfig<T>> GuaranteedDrops;
        [SerializeField] public List<DropConfig<T>> OptionalDrops;
        [SerializeField] protected int NoDropWeight = 100;

        public int OverallGuaranteedDropsWeight {
            get {
                return GuaranteedDrops.Aggregate(0, (acc, x) => acc + x.Weight);
            }
        }
        public int OverallOptionalDropsWeight {
            get {
                return OptionalDrops.Aggregate(0, (acc, x) => acc + x.Weight);
            }
        }

        /// <summary>
        /// Get's the guaranteed drops from the loot table
        /// </summary>
        /// <returns></returns>
        public List<T> GetGuaranteedDrops() {
            var result = new List<T>();

            GuaranteedDrops.ForEach(dropConfig => {
                if (dropConfig.Drop == null) return;

                var count = Random.Range(dropConfig.MinCount, dropConfig.MaxCount);
                for (int i = 0; i < count; i++) {
                    result.Add(dropConfig.Drop);
                }
            });

            return result;
        }

        /// <summary>
        /// Get's the optional drops from the loot table
        /// </summary>
        /// <returns></returns>
        public List<T> GetOptionalDrops(int count) {

            var result = new List<T>();
            for (int i = 0; i < count; i++) {
                var drop = GetOptionalDrop();
                if (drop != null) result.AddRange(drop);
            }

            return result;
        }

        /// <summary>
        /// Get's the optional drop from the loot table
        /// </summary>
        /// <returns></returns>
        public List<T> GetOptionalDrop() {
            var result = new List<T>();
            var roll = Random.Range(0, OverallOptionalDropsWeight + NoDropWeight);
            var dropsAnything = roll > NoDropWeight;

            if (!dropsAnything) return result;

            var weight = 0;
            var adjustedRoll = roll - NoDropWeight;

            for (int i = 0; i < OptionalDrops.Count; i++) {
                var dropConfig = OptionalDrops[i];
                if (weight >= adjustedRoll) {
                    result.Add(dropConfig.Drop);
                    break;
                }

                weight += dropConfig.Weight;
            }

            return result;
        }

        public float GetChanceFor(DropConfig<T> dropConfig) {
            if (dropConfig == null) return (float)NoDropWeight / (OverallOptionalDropsWeight + NoDropWeight);

            return (float)dropConfig.Weight / (OverallOptionalDropsWeight + NoDropWeight);
        }

    }

    [System.Serializable]
    public class DropConfig<T> where T : Object {
        public int Weight = 1;
        public int MinCount;
        public int MaxCount = 1;
        public T Drop;

        public override string ToString() {
            if (Drop == null) return "Missing Assignment (!)";
            var count = string.Format(" ({0} - {1})", MinCount, MaxCount);
            if (MinCount == MaxCount) {
                count = string.Format(" ({0})", MinCount);
            }

            return Drop.name + count;
        }
    }
}