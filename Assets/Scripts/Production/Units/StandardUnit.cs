using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class StandardUnit : UnitBase
{
    //[SerializeField]
    //GameObject stdUnitPrefab;

    //IPool<StandardUnit> stdUnitPool;
    
    public override UnitType Type { get; set; } = UnitType.Standard;

    //public StandardUnit RentFromPool()
    //{
    //    return stdUnitPool.Rent() as StandardUnit;
    //}
    public override void ReturnToPool()
    {
        base.ReturnToPool();
        SpawnManager.returnStdToPool?.Invoke(this);
        //stdUnitPool.UnRent(this as StandardUnit);
    }
}
