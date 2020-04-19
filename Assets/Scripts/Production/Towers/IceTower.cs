using UnityEngine;
using Tools;
using System;

public class IceTower : TowerBase
{
	[SerializeField, Tooltip("Where the projectiles go hide after despawning.")]
	Transform bulletPoolTransform;

	[SerializeField, Tooltip("Which projectile to shoot from this tower.")]
	GameObject projectilePrefab;

	IPool<IceProjectile> iceProjectilePool;

	public static Action<IceProjectile> returnToPool;

	private void Awake()
	{
		bulletPoolTransform = GameObject.Find("BulletPoolTransform").transform;
	}
	protected override void Start()
	{
		base.Start();
		returnToPool += ReturnToPool;

		SetProjectilePool();
	}

	void SetProjectilePool()
	{
		iceProjectilePool = new ObjectPool<IceProjectile>(projectilePrefab, bulletPoolTransform);
	}
	protected override void SpawnBullet()
	{
		base.SpawnBullet();

		IceProjectile bullet = iceProjectilePool.Rent() as IceProjectile;

		SetUpBullet(bullet);
	}

	void SetUpBullet(IceProjectile bullet)
	{

		bullet.SetTarget(target);
		bullet.transform.position = projectileSpawnPoint.transform.position;
		bullet.gameObject.SetActive(true);
	}

	void ReturnToPool(IceProjectile projectileToReturn)
	{
		iceProjectilePool.UnRent(projectileToReturn);
	}
}