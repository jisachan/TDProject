using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

public static class LevelData
{
    static string[] dividedMapText = MapReader.ReadTextMap().Split('#');
    public static string[] MapData { get; private set; } = dividedMapText[0].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    static string [] waveData = dividedMapText[1].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    static string[] unitData;

    public static int GetUnitNr(int currentWave, UnitType unitType)
    {
        int typeNr = 0;

        if (unitType == UnitType.Standard)
        {
            unitData = waveData[currentWave-1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            typeNr = int.Parse(unitData[0]);
        }
        else if (unitType == UnitType.Big)
        {
            unitData = waveData[currentWave-1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            typeNr = int.Parse(unitData[1]);
        }
        return typeNr;
    }
}
