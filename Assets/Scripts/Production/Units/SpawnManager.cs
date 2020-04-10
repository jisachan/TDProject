using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] units;

    float spawnTimer = 2.5f;
    int maxWaves; //maxWaves = LevelData.WaveData.Length;
    int currentWave = 1;
    int currentStdUnitNr;
    int maxStdUnitNr;
    int currentBigUnitNr;
    int maxBigUnitNr;

    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit();
        }
    }

    public void StartWave()
    {
        maxStdUnitNr = LevelData.GetUnitNr(currentWave, UnitType.Standard);
        currentStdUnitNr = 0;
        maxBigUnitNr = LevelData.GetUnitNr(currentWave, UnitType.Big);
        currentBigUnitNr = 0;
        //currentWave++;
    }

    public void SpawnUnit() 
    {
        if(currentStdUnitNr < maxStdUnitNr)
        {
            Instantiate(units[0], LevelManager.UnitSpawnTile);
            currentStdUnitNr++;
        }

        if (currentBigUnitNr < maxBigUnitNr)
        {
            Instantiate(units[1], LevelManager.UnitSpawnTile);
            currentBigUnitNr++;
        }
    }

    public void DespawnUnit()
    {
        //use fancy pooling thingies
    }
}
