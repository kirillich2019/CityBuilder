using System;
using Models;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public sealed class ResourceWallet : MonoBehaviour
    {
        [Inject]
        private StorageKeeper storageKeeper;
        public event Action<Resource> ResourceAmountOrCapacityUpdated;
        
        private void OnEnable()
        {
            storageKeeper.GasStorages.OnTotalCapacityUpdated += RecourceCapacityUpdated;
            storageKeeper.MineralStorages.OnTotalCapacityUpdated += RecourceCapacityUpdated;
        }

        private void OnDisable()
        {
            storageKeeper.GasStorages.OnTotalCapacityUpdated -= RecourceCapacityUpdated;
            storageKeeper.MineralStorages.OnTotalCapacityUpdated -= RecourceCapacityUpdated;
        }

        public int GetAmountResource(Resource resource)
        {
            switch (resource)
            {
                case Resource.Gas:
                    return storageKeeper.GasStorages.ToatalFullness;
                case Resource.Mineral:
                    return storageKeeper.MineralStorages.ToatalFullness;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }
        }

        public int GetCapacityResource(Resource resource)
        {
            switch (resource)
            {
                case Resource.Gas:
                    return storageKeeper.GasStorages.TotalCapacity;
                case Resource.Mineral:
                    return storageKeeper.MineralStorages.TotalCapacity;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }
        }

        /// <summary>
        /// Добавляет реусрсы в хранилище,
        /// возвращает остаток если в хранилище не хватило места 
        /// </summary>
        /// <param name="resource">Тип ресурса</param>
        /// <param name="count">Количество реусурса</param>
        /// <returns>Остаток, если в хранилище поместилось всё то он равен 0</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int TryAddResource(Resource resource, int count)
        {
            int reminder;

            switch (resource)
            {
                case Resource.Gas:
                    reminder = storageKeeper.GasStorages.TryAddRecource(count);
                    break;
                case Resource.Mineral:
                    reminder = storageKeeper.MineralStorages.TryAddRecource(count);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }

            if (reminder != count)
                InvokeResourceAmountOrCapacityUpdated(resource);

            return reminder;
        }

        /// <summary>
        /// Eсли в хранилищах достаточно ресурса вычитает количетво ресурса из хранилищ и возвращает True.
        /// Если в хранилищах не достаёт реусурса возвращает False.
        /// </summary>
        /// <param name="resource">Тип реусерса</param>
        /// <param name="count">Количество реуса</param>
        /// <returns>Успешность операции</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool TryGetRecource(Resource resource, int count)
        {
            bool result;
            
            switch (resource)
            {
                case Resource.Gas:
                    result = storageKeeper.GasStorages.TryGetRecource(count);
                    break;
                case Resource.Mineral:
                    result = storageKeeper.MineralStorages.TryGetRecource(count);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }

            if(result)
                InvokeResourceAmountOrCapacityUpdated(resource);
            
            return result;
        }

        private void RecourceCapacityUpdated(Resource resource)
        {
            InvokeResourceAmountOrCapacityUpdated(resource);
        }

        private void InvokeResourceAmountOrCapacityUpdated(Resource resource)
        {
            ResourceAmountOrCapacityUpdated?.Invoke(resource);
        }
    }
}