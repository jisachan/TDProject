using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class BigUnit : UnitBase
{
    //[SerializeField]
    //GameObject bigUnitPrefab;

    //IPool<BigUnit> bigUnitPool;

    public override UnitType Type { get; set; } = UnitType.Big;

    //public BigUnit RentFromPool()
    //{
    //    return bigUnitPool.Rent() as BigUnit;
    //}
    public override void ReturnToPool()
    {
        base.ReturnToPool();
        SpawnManager.returnBigToPool?.Invoke(this);
        //bigUnitPool.UnRent(this as BigUnit);
    }
}
