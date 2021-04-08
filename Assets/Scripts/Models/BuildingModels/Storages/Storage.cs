using Controllers;
using UnityEngine;
using Zenject;

namespace Models.Storages
{
    public abstract class Storage : Building
    {
        [Inject] protected StorageKeeper storageKeeper;
        [SerializeField] private StorageData storageData;
        public StorageData Data => storageData;
        
        public int TryAddResource(int count)
        {
            if (count <= 0)
                return count;

            if (Data.CurrentFullness >= Data.MaxCapacity)
                return count;

            var summ = Data.CurrentFullness + count;
            if (summ > Data.MaxCapacity)
            {
                Data.CurrentFullness = Data.MaxCapacity;
                return summ - Data.MaxCapacity;
            }

            Data.CurrentFullness = summ;
            return 0;
        }
        
        public int TryGetResource(int count)
        {
            if (count <= 0)
                return count;

            if (Data.CurrentFullness >= Data.MaxCapacity)
                return count;

            var summ = Data.CurrentFullness - count;
            if (summ > 0)
            {
                Data.CurrentFullness = summ;
                return 0;
            }

            Data.CurrentFullness = 0;
            return Mathf.Abs(summ);
        }
    }
}