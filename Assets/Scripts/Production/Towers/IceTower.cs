using UnityEngine;
using Tools;

public class IceTower : TowerBase
{
	[SerializeField]
	GameObject iceProjectilePrefab;

	IPool<IceProjectile> iceProjectilePool;

	protected override void Start()
	{
		base.Start();
		iceProjectilePool = new ObjectPool<IceProjectile>(iceProjectilePrefab, transform);
	}

	protected override void SpawnBullet()
	{
		base.SpawnBullet();
		IceProjectile bullet = iceProjectilePool.Rent() as IceProjectile;

		bullet.SetTarget(target);
		bullet.transform.position = transform.position;
		bullet.gameObject.SetActive(true);
	}

	public override void ReturnToPool(ProjectileBase projectileToReturn)
	{
		base.ReturnToPool(projectileToReturn);
		iceProjectilePool.UnRent(projectileToReturn as IceProjectile);
	}
}