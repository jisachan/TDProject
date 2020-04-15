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

    }

    public void StartWave()
    {
        maxStdUnitNr = LevelData.GetUnitNr(currentWave, UnitType.Standard);
        currentStdUnitNr = 0;
        maxBigUnitNr = LevelData.GetUnitNr(currentWave, UnitType.Big);
        currentBigUnitNr = 0;

        StartCoroutine(SpawnUnit());

        //currentWave++;
    }

    public IEnumerator SpawnUnit() 
    {
        while (true)
        {
            //would be fun to make a random enemy spawn, rather than always std >>> big.
            //if there is time...
            if (currentStdUnitNr < maxStdUnitNr)
            {
                GameObject stdUnit = Instantiate(units[0]);
                stdUnit.transform.position = LevelManager.UnitSpawnTile.WorldPosition;
                currentStdUnitNr++;
            }

            else if (currentBigUnitNr < maxBigUnitNr)
            {
                GameObject bigUnit = Instantiate(units[1]);
                bigUnit.transform.position = LevelManager.UnitSpawnTile.WorldPosition;
                currentBigUnitNr++;
            }

            if (currentStdUnitNr == maxStdUnitNr && currentBigUnitNr == maxBigUnitNr)
            {
                yield break;
            }

            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
