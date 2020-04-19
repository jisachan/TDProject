public class BigUnit : UnitBase
{
    public override UnitType Type { get; set; } = UnitType.Big;

    public override void ReturnToPool()
    {
        base.ReturnToPool();
        UnitSpawnManager.returnBigToPool?.Invoke(this);
    }
}
