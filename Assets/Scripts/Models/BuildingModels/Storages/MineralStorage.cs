namespace Models.Storages
{
    public class MineralStorage : Storage
    {
        public override void OnBuild() => storageKeeper.MineralStorages.AddStorage(this);
    }
}