using UnityEngine;
using Tools;

public class FireTower : TowerBase
{
	[SerializeField]
	GameObject fireProjectilePrefab;

	IPool<FireProjectile> fireProjectilePool;

	protected override void Start()
	{
		base.Start();
		fireProjectilePool = new ObjectPool<FireProjectile>(fireProjectilePrefab, transform);
	}

	protected override void SpawnBullet()
	{
		base.SpawnBullet();

		FireProjectile bullet = fireProjectilePool.Rent() as FireProjectile;

		bullet.SetTarget(target);
		bullet.transform.position = transform.position;
		bullet.gameObject.SetActive(true);
	}

	public override void ReturnToPool(ProjectileBase projectileToReturn)
	{
		base.ReturnToPool(projectileToReturn);
		fireProjectilePool.UnRent(projectileToReturn as FireProjectile);
	}
}