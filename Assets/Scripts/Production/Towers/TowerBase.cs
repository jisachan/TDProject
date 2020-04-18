using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public abstract class TowerBase : MonoBehaviour
{
	[SerializeField]
	float firingTimer = 3;

	[SerializeField]
	float range = 2f;

	[HideInInspector]
	public UnitBase target;

	public static List<UnitBase> targets = new List<UnitBase>();

	public static List<UnitBase> targetsWithinRange = new List<UnitBase>();

	public static Action<ProjectileBase> returnToPool;

	// Start is called before the first frame update
	protected virtual void Start()
	{
		returnToPool += ReturnToPool;

		StartCoroutine(ShootBullet());
	}

	// Update is called once per frame
	void Update()
	{
		//add to observer thingie
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

	//wont it cause problems if this is projectilebase when pool doesnt use it?
	public virtual void ReturnToPool(ProjectileBase projectileToReturn)
	{

	}
}
