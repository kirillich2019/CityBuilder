using Controllers;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ResourceWallet wallet;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StorageKeeper storageKeeper;
    [SerializeField] private BuildingBuilder buildingBuilder;
        
    public override void InstallBindings()
    {
        Container.Bind<ResourceWallet>().FromInstance(wallet);
        Container.Bind<PlayerInput>().FromInstance(playerInput);
        Container.Bind<StorageKeeper>().FromInstance(storageKeeper);
        Container.Bind<BuildingBuilder>().FromInstance(buildingBuilder);
    }
}