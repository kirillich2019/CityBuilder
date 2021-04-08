using System;
using System.Collections.Generic;
using Models;
using Models.Storages;

namespace DefaultNamespace.Controllers
{
    [Serializable]
    public class StorageGroup<T> where T : Storage
    {
        public event Action<Resource> OnTotalCapacityUpdated;

        private int initialCapacity;
        private int initialDeposit;
        private List<T> storages;

        public StorageGroup(int initialCapacity)
        {
            this.initialCapacity = initialCapacity;
            initialDeposit = initialCapacity;
        }

        public int TotalCapacity
        {
            get
            {
                if (storages == null)
                    return initialCapacity;

                var summ = initialCapacity;
                storages.ForEach(s => summ += s.Data.MaxCapacity);
                return summ;
            }
        }

        public int ToatalFullness
        {
            get
            {
                if (storages == null)
                    return initialDeposit;

                var summ = initialDeposit;
                storages.ForEach(s => summ += s.Data.CurrentFullness);
                return summ;
            }
        }

        public void AddStorage(T newStorage)
        {
            if (newStorage == null)
                throw new ArgumentNullException(nameof(newStorage));

            if (storages != null && storages.Contains(newStorage))
                throw new Exception("Already contains this storage");

            if (storages == null) storages = new List<T>();
            storages.Add(newStorage);
            InvokeOnTotalCapacityUpdated(newStorage.Data.Resource);
        }

        public void RemoveStorage(T storage)
        {
            if (storages == null)
                return;

            if (!storages.Contains(storage))
                throw new Exception("Does not contain this storage");

            storages.Remove(storage);
            InvokeOnTotalCapacityUpdated(storage.Data.Resource);
        }

        public int TryAddRecource(int count)
        {
            var remainder = count;

            if (initialDeposit < initialCapacity)
            {
                var summ = initialDeposit + remainder;
                if (summ > initialCapacity)
                {
                    initialDeposit = initialCapacity;
                    remainder = summ - initialCapacity;
                }
                else
                {
                    initialDeposit = summ;
                    remainder = 0;
                }
            }

            if (storages == null)
                return remainder;
            
            foreach (var storage in storages)
            {
                remainder = storage.TryAddResource(remainder);
                if (remainder == 0)
                    return 0;
            }

            return remainder;
        }

        public bool TryGetRecource(int count)
        {
            if (ToatalFullness < count)
                return false;

            var remainder = count;

            initialDeposit -= remainder;

            if (storages == null)
                return true;

            foreach (var storage in storages)
            {
                remainder = storage.TryGetResource(remainder);
                if (remainder == 0)
                    return true;
            }

            return true;
        }

        protected virtual void InvokeOnTotalCapacityUpdated(Resource resource)
        {
            OnTotalCapacityUpdated?.Invoke(resource);
        }
    }
}