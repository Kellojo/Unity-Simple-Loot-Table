﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kellojo.SimpleLootTable {

    public abstract class LootTableBase<T> : ScriptableObject where T : Object {


        [SerializeField] public List<DropConfig<T>> GuaranteedDrops = new List<DropConfig<T>>();
        [SerializeField] public List<WeightedDropConfig<T>> OptionalDrops = new List<WeightedDropConfig<T>>();
        [SerializeField] protected int NoDropWeight = 100;

        public int OverallOptionalDropsWeight {
            get {
                return OptionalDrops.Aggregate(0, (acc, x) => acc + x.Weight);
            }
        }

        protected void OnValidate() {
            if (NoDropWeight < 0) NoDropWeight = 0;
            GuaranteedDrops.ForEach(drop => drop.OnValidate());
            OptionalDrops.ForEach(drop => drop.OnValidate());
        }

        /// <summary>
        /// Get's the guaranteed drops from the loot table
        /// </summary>
        /// <returns></returns>
        public List<T> GetGuaranteedDrops() {
            var result = new List<T>();

            GuaranteedDrops.ForEach(dropConfig => {
                if (dropConfig.Drop == null) return;

                var count = GetCountFor(dropConfig);
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
            var dropsAnything = roll >= NoDropWeight;

            if (!dropsAnything) return result;

            var adjustedRoll = roll - NoDropWeight;
            for (int i = 0; i < OptionalDrops.Count; i++) {
                var dropConfig = OptionalDrops[i];
                adjustedRoll -= dropConfig.Weight;

                if (adjustedRoll > 0) continue;

                var count = GetCountFor(dropConfig);
                for (int j = 0; j < count; j++) {
                    result.Add(dropConfig.Drop);
                }

                break;
            }

            return result;
        }

        public int GetCountFor(DropConfig<T> dropConfig) {
            return Random.Range(dropConfig.MinCount, dropConfig.MaxCount + 1);
        }

        public float GetChanceFor(WeightedDropConfig<T> dropConfig) {
            if (dropConfig == null) {
                if (NoDropWeight == 0) return 0f;
                return (float)NoDropWeight / (OverallOptionalDropsWeight + NoDropWeight);
            }

            return (float)dropConfig.Weight / (OverallOptionalDropsWeight + NoDropWeight);
        }

    }

    [System.Serializable]
    public class WeightedDropConfig<T> : DropConfig<T> where T : Object  {
        public int Weight = 1;

        public override void OnValidate() {
            base.OnValidate();
            if (Weight <= 0) Weight = 1;
        }
    }

    [System.Serializable]
    public class DropConfig<T> where T : Object {
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

        public virtual void OnValidate() {
            if (MinCount < 0) MinCount = 0;
            if (MaxCount <= 0) MaxCount = 1;
        }
    }
}