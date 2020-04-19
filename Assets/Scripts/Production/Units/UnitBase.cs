using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
	[SerializeField, Range(0.05f, 0.5f), Tooltip("How close the unit needs to be to the destination before going towards the next destination.")]
	protected float accuracy = 0.1f;

	[SerializeField, Tooltip("Unit's movement speed.")]
	private float speed = 2;

	[SerializeField, Tooltip("Unit's health.")]
	protected float health = 100;

	public abstract UnitType Type { get; set; }

	public static GridPoint GridPosition { get; set; }

	public Vector3 Destination { get => destination; set => destination = value; }

	public float Speed { get => speed; set => speed = value; }

	public int Strength { get => strength; set => strength = value; }

	public bool Slowed { get => slowed; set => slowed = value; }

	[Tooltip("How much damage the unit does upon reaching the player base.")]
	public int strength = 1;

	bool slowed = false;

	float tempHealth;

	float tempSpeed;

	Vector3 destination;

	protected Stack<Node> path;

	private void OnEnable()
	{
		tempHealth = health;
		tempSpeed = speed;
		SetPath(LevelManager.FinalPath);
		TowerBase.targets.Add(this);
	}

	private void OnDisable()
	{
		TowerBase.targets.Remove(this);

		if (TowerBase.targetsWithinRange.Contains(this))
		{
			TowerBase.targetsWithinRange.Remove(this);
		}
		health = tempHealth;
		speed = tempSpeed;
	}

	void Update()
	{
		//Possible to put this outside of Update??
		CheckDistanceToPlayerBase();
	}
	void LateUpdate()
	{
		Move();
		
		//this could be put in the Move function
		//but they look cuter when it's here...
		//(could be smoother with lerp too, but again: this is cuter!)
		//cuteness > good code
		Rotate();
	}

	void CheckDistanceToPlayerBase()
	{
		if (Mathf.Approximately(Vector3.Distance(transform.position, destination), 0))
		{
			Player.reachedPlayerBase?.Invoke(this);
		}
	}

	public void SetPath(Stack<Node> newPath)
	{
		if (newPath != null)
		{
			path = newPath;

			GridPosition = path.Peek().GridPosition;
			Destination = path.Pop().WorldPosition;
		}
	}

	private void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, Destination, Speed * Time.deltaTime);

		if (Vector3.Distance(transform.position, Destination) < accuracy)
		{
			if (path != null && path.Count > 0)
			{
				GridPosition = path.Peek().GridPosition;
				Destination = path.Pop().WorldPosition;
			}
		}
	}

	private void Rotate()
	{
		if (path.Count > 0)
		{
			transform.LookAt(path.Peek().TileRef.transform);
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			ReturnToPool();
		}
	}

	public virtual void ReturnToPool()
	{
	}
}