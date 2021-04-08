using System;
using UnityEngine;

namespace Models.BuildingModels
{
    [Serializable]
    public class MineData : BuildingData
    {
        [SerializeField] private int miningTimeInSeconds;
        [SerializeField] private int resourceAmount;
        [SerializeField] private Resource resource;
        [SerializeField] private GameObject resourcePrefab;
        
        public int MiningTimeInSeconds => miningTimeInSeconds;

        public int ResourceAmount => resourceAmount;

        public GameObject ResourcePrefab => resourcePrefab;

        public Resource Resource => resource;
    }
}