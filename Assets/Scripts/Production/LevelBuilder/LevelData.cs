using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

public static class LevelData
{
    static string[] dividedMapText = MapReader.ReadTextMap().Split('#');
    public static string[] MapData { get; private set; } = dividedMapText[0].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    public static string[] WaveData { get; private set; } = dividedMapText[1].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    static string[] unitData;

    public static int GetUnitNr(int currentWave, UnitType unitType)
    {
        int typeNr = 0;

        if (unitType == UnitType.Standard)
        {
            unitData = WaveData[currentWave-1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            typeNr = int.Parse(unitData[0]);
        }
        else if (unitType == UnitType.Big)
        {
            unitData = WaveData[currentWave-1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            typeNr = int.Parse(unitData[1]);
        }
        return typeNr;
    }
}
