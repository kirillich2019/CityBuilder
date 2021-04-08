using System;
using UnityEngine;

namespace Models.Storages
{
    [Serializable]
    public class StorageData : BuildingData
    {
        [SerializeField] private int maxCapacity;
        [SerializeField] private int currentFullness;
        [SerializeField] private Resource resource;
        
        public int MaxCapacity => maxCapacity;

        public int CurrentFullness
        {
            get => currentFullness;
            set => currentFullness = value;
        }

        public Resource Resource => resource;
    }
}