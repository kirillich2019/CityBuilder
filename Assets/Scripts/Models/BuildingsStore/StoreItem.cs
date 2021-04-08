using System;
using UnityEngine;

namespace Models.BuildingsStore
{
    [Serializable]
    public class StoreItem
    {
        [SerializeField] private Building building;
        [SerializeField] private int cost;
        [SerializeField] private Resource resource;

        public Building Building => building;

        public int Cost => cost;

        public Resource Resource => resource;
    }
}