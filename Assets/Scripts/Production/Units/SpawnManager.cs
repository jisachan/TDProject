using System;
using System.Collections;
using UnityEngine;
using Tools;

public class SpawnManager : MonoBehaviour
{
	float spawnTimer = 2.5f;
	int maxWaves;
	int currentWave = 1;
	int currentStdUnitNr;
	int maxStdUnitNr;
	int currentBigUnitNr;
	int maxBigUnitNr;
	bool allUnitsSpawned;

	// gwang
	[SerializeField]
	GameObject stdUnit;
	[SerializeField]
	GameObject bigUnit;
	IPool<StandardUnit> stdUnitPool;
	IPool<BigUnit> bigUnitPool;
	
	public static Action<StandardUnit> returnStdToPool;
	public static Action<BigUnit> returnBigToPool;

	// Start is called before the first frame update
	void Start()
	{
		returnStdToPool += ReturnStdUnitToPool;
		returnBigToPool += ReturnBigUnitToPool;

		// gwang
		stdUnitPool = new ObjectPool<StandardUnit>(stdUnit, transform);
		bigUnitPool = new ObjectPool<BigUnit>(bigUnit, transform);
		
		maxWaves = LevelData.WaveData.Length;
		Debug.Log("maxWaves: " + maxWaves);
		StartWave();
	}

	public void StartWave()
	{
		allUnitsSpawned = false;
		maxStdUnitNr = LevelData.GetUnitNr(currentWave, UnitType.Standard);
		currentStdUnitNr = 0;
		maxBigUnitNr = LevelData.GetUnitNr(currentWave, UnitType.Big);
		currentBigUnitNr = 0;

		StartCoroutine(SpawnUnit());

	}

	public IEnumerator SpawnUnit()
	{
		while (true)
		{
			//would be fun to make a random enemy spawn, rather than always std >>> big.
			//if there is time...
			//would also be nice if they turned to the front...
			if (allUnitsSpawned == false)
			{
				if (currentStdUnitNr < maxStdUnitNr)
				{
					//gwang
					StandardUnit unit = stdUnitPool.Rent() as StandardUnit;
					unit.transform.position = LevelManager.UnitSpawnTile.pathPosition;
					unit.gameObject.SetActive(true);

					//StandardUnit stdUnit = 
					//stdUnit.RentFromPool();
					//stdUnit.transform.position = LevelManager.UnitSpawnTile.pathPosition;
					currentStdUnitNr++;
				}

				else if (currentBigUnitNr < maxBigUnitNr)
				{
					//gwang
					BigUnit unit = bigUnitPool.Rent() as BigUnit;
					unit.transform.position = LevelManager.UnitSpawnTile.pathPosition;
					unit.gameObject.SetActive(true);

					//BigUnit bigUnit = bigUnitPool.Rent() as BigUnit;
					//bigUnit.RentFromPool();
					//bigUnit.transform.position = LevelManager.UnitSpawnTile.pathPosition;
					currentBigUnitNr++;
				}
			}

			if (currentStdUnitNr == maxStdUnitNr && currentBigUnitNr == maxBigUnitNr)
			{
				allUnitsSpawned = true;
				if (TowerBase.targets.Count == 0)
				{allUnitsSpawned = true;
					if (currentWave != maxWaves)
					{
						currentWave++;
						StartWave();
						yield break;
					}
					else
					{
						GameManager.StartNextLevel();
					}
				}
			}

			yield return new WaitForSeconds(spawnTimer);
		}
	}

	void ReturnStdUnitToPool(StandardUnit unit)
	{
		stdUnitPool.UnRent(unit);
		unit.gameObject.SetActive(false);
	}

	void ReturnBigUnitToPool(BigUnit unit)
	{
		bigUnitPool.UnRent(unit);
		unit.gameObject.SetActive(false);
	}
}