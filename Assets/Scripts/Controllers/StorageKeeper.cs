using DefaultNamespace.Controllers;
using Models.Storages;
using UnityEngine;

namespace Controllers
{
    public class StorageKeeper : MonoBehaviour
    {
        private readonly StorageGroup<GasStorage> gasStorages = new StorageGroup<GasStorage>(150);
        private readonly StorageGroup<MineralStorage> mineralStorages = new StorageGroup<MineralStorage>(150);

        public StorageGroup<GasStorage> GasStorages => gasStorages;
        public StorageGroup<MineralStorage> MineralStorages => mineralStorages;
    }
}