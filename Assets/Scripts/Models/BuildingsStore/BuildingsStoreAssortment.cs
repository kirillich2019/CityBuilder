using UnityEngine;

namespace Models.BuildingsStore
{
    [CreateAssetMenu(fileName = "BuildingsStoreAssortment", menuName = "ScriptableObjects/BuildingsStoreAssortment", order = 0)]
    public class BuildingsStoreAssortment : ScriptableObject
    {
        [SerializeField] private StoreItem[] buildings;

        public StoreItem[] Buildings => buildings;
    }
}