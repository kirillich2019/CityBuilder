using System;
using Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Models.BuildingModels
{
    public abstract class Mine : Building
    {
        [SerializeField] protected MineData data;
        [Inject] private ResourceWallet wallet;
        private MiningState currentState;
        
        public override void OnBuild()
        {
            MiningCycle();
        }

        protected virtual async void MiningCycle()
        {
            while (true)
            {
                currentState = MiningState.Mining;
                await UniTask.Delay(TimeSpan.FromSeconds(data.MiningTimeInSeconds));
                currentState = MiningState.Ready;
                var marker = Instantiate(data.ResourcePrefab, transform.position + Vector3.up, Quaternion.identity);
                await UniTask.WaitWhile(() => currentState == MiningState.Ready);
                Destroy(marker);
            }
        }

        private void OnMouseDown()
        {
            if(currentState != MiningState.Ready)
                return;
            
            var reminder = wallet.TryAddResource(data.Resource, data.ResourceAmount);
            if (reminder == 0)
                currentState = MiningState.Mining;
        }
    }
}