using System;
using System.Collections;
using UnityEngine;
using Tools;

public class UnitSpawnManager : MonoBehaviour
{
	float spawnTimer = 2.5f;
	int maxWaves;
	int currentWave = 1;
	int currentStdUnitNr;
	int maxStdUnitNr;
	int currentBigUnitNr;
	int maxBigUnitNr;
	bool allUnitsSpawned;

	[SerializeField, Tooltip("Standard Unit for this game.")]
	GameObject stdUnit;

	[SerializeField, Tooltip("Big Unit for this game.")]
	GameObject bigUnit;

	IPool<StandardUnit> stdUnitPool;
	IPool<BigUnit> bigUnitPool;

	public static Action<StandardUnit> returnStdToPool;
	public static Action<BigUnit> returnBigToPool;

	void Start()
	{
		returnStdToPool += ReturnStdUnitToPool;
		returnBigToPool += ReturnBigUnitToPool;

		stdUnitPool = new ObjectPool<StandardUnit>(stdUnit, transform);
		bigUnitPool = new ObjectPool<BigUnit>(bigUnit, transform);

		maxWaves = LevelData.WaveData.Length;

		StartWave();
	}

	public void StartWave()
	{
		allUnitsSpawned = false;

		SetMaxStdUnitNr();
		currentStdUnitNr = 0;

		SetMaxBigUnitNr();
		currentBigUnitNr = 0;

		StartCoroutine(SpawnUnit());
	}

	public void SetMaxStdUnitNr()
	{
		maxStdUnitNr = LevelData.GetUnitNr(currentWave, UnitType.Standard);
	}

	public void SetMaxBigUnitNr()
	{
		maxBigUnitNr = LevelData.GetUnitNr(currentWave, UnitType.Big);
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
					StandardUnit stdUnit = stdUnitPool.Rent() as StandardUnit;
					SetUpUnit(stdUnit);
					currentStdUnitNr++;
				}

				else if (currentBigUnitNr < maxBigUnitNr)
				{
					BigUnit bigUnit = bigUnitPool.Rent() as BigUnit;
					SetUpUnit(bigUnit);
					currentBigUnitNr++;
				}
			}

			if (currentStdUnitNr == maxStdUnitNr && currentBigUnitNr == maxBigUnitNr)
			{
				allUnitsSpawned = true;

				//Making sure all targets are gone before starting next wave.
				if (TowerBase.targets.Count == 0)
				{
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

	void SetUpUnit(UnitBase unit)
	{
		unit.transform.position = LevelManager.UnitSpawnTile.PathPosition;
		unit.gameObject.SetActive(true);
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