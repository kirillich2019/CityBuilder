namespace Models.Storages
{
    public class GasStorage : Storage
    {
        public override void OnBuild() => storageKeeper.GasStorages.AddStorage(this);
    }
}