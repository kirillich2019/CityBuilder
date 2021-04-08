using Controllers;
using Models.BuildingsStore;
using UnityEngine;
using Zenject;

namespace UI
{
    public class BuildingStoreView : MonoBehaviour
    {
        [SerializeField] private BuildingsStoreAssortment storeAssortment;
        [SerializeField] private StoreItemView itemViewPrefab;
        [SerializeField] private RectTransform itemsParent;

        private PlayerInput input;
        private BuildingBuilder builder;
        private ResourceWallet wallet;
        
        [Inject]
        public void Initalize(PlayerInput input, BuildingBuilder builder, ResourceWallet wallet)
        {
            this.input = input;
            this.builder = builder;
            this.wallet = wallet;
        }
        
        private void Start()
        {
            InstantiateItems();
        }

        private void InstantiateItems()
        {
            var buildings = storeAssortment.Buildings;
            foreach (var building in buildings) Instantiate(itemViewPrefab, itemsParent).Initialize(building, BuyButtonClickHandler);
        }

        private async void BuyButtonClickHandler(StoreItem storeItem)
        {
            if(!builder.CanBuild)
                return;
            
            if(wallet.GetAmountResource(storeItem.Resource) < storeItem.Cost)
                return;

            wallet.TryGetRecource(storeItem.Resource, storeItem.Cost);
            gameObject.SetActive(false);
            input.enabled = true;
            await builder.Build(storeItem.Building);
            input.enabled = false;
            gameObject.SetActive(true);
        }
    }
}