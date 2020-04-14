using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardUnit : UnitBase
{
    public override UnitType Type { get; set; } = UnitType.Standard;

    void Awake()
    {
        speed = 2;
        health = 100;
    }
}
