using System;
using Controllers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainView : MonoBehaviour
    {
        public ResourceWallet wallet;
        [SerializeField] private BuildingStoreView storeView;
        [SerializeField] private Text gasCount;
        [SerializeField] private Text mineralCount;

        private void OnEnable()
        {
            wallet.ResourceAmountOrCapacityUpdated += WalletOnResourceAmountOrCapacityUpdated;
        }

        private void OnDisable()
        {
            wallet.ResourceAmountOrCapacityUpdated -= WalletOnResourceAmountOrCapacityUpdated;
        }

        private void Start()
        {
            UpdateGasView();
            UpdateMineralView();
        }

        private void WalletOnResourceAmountOrCapacityUpdated(Resource resource)
        {
            switch (resource)
            {
                case Resource.Gas:
                    UpdateGasView();
                    break;
                case Resource.Mineral:
                    UpdateMineralView();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }
        }

        private void UpdateGasView() => gasCount.text = $"{wallet.GetAmountResource(Resource.Gas)}/{wallet.GetCapacityResource(Resource.Gas)}";

        private void UpdateMineralView() => mineralCount.text = $"{wallet.GetAmountResource(Resource.Mineral)}/{wallet.GetCapacityResource(Resource.Mineral)}";

        public void OpenStoreButtonClickHandler()
        {
            storeView.gameObject.SetActive(true);
        }
    }
}