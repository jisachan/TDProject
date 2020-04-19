using UnityEngine;
using Tools;
using System;

public class FireTower : TowerBase
{
	[SerializeField, Tooltip("Where the projectiles go hide after despawning.")]
	Transform projectilePoolTransform;

	[SerializeField, Tooltip("Which projectile to shoot from this tower.")]
	GameObject projectilePrefab;

	IPool<FireProjectile> fireProjectilePool;

	public static Action<FireProjectile> returnToPool;

	private void Awake()
	{
		projectilePoolTransform = GameObject.Find("BulletPoolTransform").transform;
	}

	protected override void Start()
	{
		base.Start();
		returnToPool += ReturnToPool;

		SetProjectilePool();
	}

	void SetProjectilePool()
	{
		fireProjectilePool = new ObjectPool<FireProjectile>(projectilePrefab, projectilePoolTransform);
	}

	protected override void SpawnBullet()
	{
		base.SpawnBullet();

		FireProjectile bullet = fireProjectilePool.Rent() as FireProjectile;
		
		SetUpBullet(bullet);		
	}

	void SetUpBullet(FireProjectile bullet)
	{
		if (bullet)
		{
			bullet.SetTarget(target);
			bullet.transform.position = projectileSpawnPoint.transform.position;
			bullet.gameObject.SetActive(true);
		}
	}

	void ReturnToPool(FireProjectile projectileToReturn)
	{
		fireProjectilePool.UnRent(projectileToReturn);
	}
}