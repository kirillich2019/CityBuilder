using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models;
using Models.BuildingsStore;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class BuildingBuilder : MonoBehaviour
    {
        [SerializeField] private BuildingsStoreAssortment storeAssortment;
        [SerializeField] private GameObject scaffoldingPrefab;

        [Inject]
        private PlayerInput playerInput;
        private bool canBuild = true;
        private Vector3? buildPos;

        public bool CanBuild => canBuild;

        private void OnEnable()
        {
            playerInput.PlayerPressedField += OnPlayerPressedField;
        }

        private void OnDisable()
        {
            playerInput.PlayerPressedField -= OnPlayerPressedField;
        }

        private void OnPlayerPressedField(Vector3 pos)
        {
            buildPos = pos;
        }

        public async Task Build(Building newBuildingPrefab)
        {
            if (!CanBuild)
                return;

            await UniTask.WaitWhile(() => buildPos == null);
            
            Build(newBuildingPrefab, buildPos.Value);
            buildPos = null;
        }

        private async void Build(Building newBuildingPrefab, Vector3 position)
        {
            canBuild = false;
            
            var pos = position + Vector3.up;
            var scaffolding = Instantiate(scaffoldingPrefab, pos, Quaternion.identity);
            var newBuilding = Instantiate(newBuildingPrefab, pos, Quaternion.identity);
            
            await UniTask.Delay(TimeSpan.FromSeconds(newBuildingPrefab.BuildTimeInSeconds));
            
            newBuilding.OnBuild();
            Destroy(scaffolding);
            
            canBuild = true;
        }
    }
}