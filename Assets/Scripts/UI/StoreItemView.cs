using System;
using Models.BuildingsStore;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StoreItemView : MonoBehaviour
    {
        [SerializeField] private Text itemName;
        [SerializeField] private Text itemCost;
        private Action<StoreItem> onBuyButtonClickAction;
        private StoreItem item;
        
        public void Initialize(StoreItem item, Action<StoreItem> onBuyButtonClickAction)
        {
            this.item = item ?? throw new ArgumentNullException(nameof(item));
            this.onBuyButtonClickAction = onBuyButtonClickAction ?? throw new ArgumentNullException(nameof(onBuyButtonClickAction));
            
            itemName.text = item.Building.name;
            itemCost.text = $"{item.Cost} {item.Resource}";
        }

        public void BuyButtonClickHandler() => onBuyButtonClickAction.Invoke(item);
    }
}