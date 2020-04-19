using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
	[SerializeField, Tooltip("How often the tower fires a projectile.")]
	float firingTimer = 3;

	[SerializeField, Tooltip("How close the target needs to be for the tower to shoot at it.")]
	float range = 2f;
	
	[SerializeField, Tooltip("Where on the tower to spawn the projectile.")]
	protected Transform projectileSpawnPoint;

	[HideInInspector]
	public UnitBase target;

	public static List<UnitBase> targets = new List<UnitBase>();

	public static List<UnitBase> targetsWithinRange = new List<UnitBase>();

	protected virtual void Start()
	{
		StartCoroutine(ShootBullet());
	}

	void Update()
	{
		//Possible to put this outside of Update??
		GetTargetInFront();
	}

	private void GetTargetInFront()
	{
		foreach (var target in targets)
		{
			if (Vector3.Distance(transform.position, target.transform.position) < range)
			{
				if (!targetsWithinRange.Contains(target))
				{
					targetsWithinRange.Add(target);
				}
			}
			else
			{
				if (targetsWithinRange.Contains(target))
				{
					targetsWithinRange.Remove(target);
				}
			}
		}
		if (targetsWithinRange.Count != 0)
		{
			target = targetsWithinRange[0];
		}
		else
		{
			target = null;
		}
	}

	IEnumerator ShootBullet()
	{
		while (true)
		{
			if (target)
			{
				if (target.isActiveAndEnabled)
				{
					SpawnBullet();
				}
				yield return new WaitForSeconds(firingTimer);
			}
			else
			{
				yield return null;
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, range);

		if (target)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position, target.transform.position);
		}
	}

	protected virtual void SpawnBullet()
	{

	}
}
