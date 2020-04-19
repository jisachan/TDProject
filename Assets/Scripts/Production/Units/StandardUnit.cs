public class StandardUnit : UnitBase
{
    public override UnitType Type { get; set; } = UnitType.Standard;

    public override void ReturnToPool()
    {
        base.ReturnToPool();
        UnitSpawnManager.returnStdToPool?.Invoke(this);
    }
}
