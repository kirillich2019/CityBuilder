using UnityEngine;

namespace Models
{
    public abstract class BuildingData
    {
        [SerializeField] private int level;

        public int Level => level;
    }
}