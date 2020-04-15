using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
	[SerializeField]
	float firingTimer = 3;
	//float damage;

	[SerializeField]
	float range = 2f;

	[SerializeField]
	ProjectileBase projectile;

	UnitBase target;

	public static List<UnitBase> targets = new List<UnitBase>();

	private List<UnitBase> targetsWithinRange = new List<UnitBase>();

	// Start is called before the first frame update
	void Start()
	{
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
				//shootbulletstuff at target
				SpawnBullet();
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

	void SpawnBullet()
	{
		Debug.Log("pew pew!");
		ProjectileBase bullet = Instantiate(projectile);
		bullet.SetTarget(target);
	}
}
