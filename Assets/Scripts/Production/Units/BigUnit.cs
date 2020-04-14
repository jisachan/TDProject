using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigUnit : UnitBase
{
    public override UnitType Type { get; set; } = UnitType.Big;

    void Awake()
    {
        speed = 1;
        health = 200;
    }
}
