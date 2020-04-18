using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
	[SerializeField]
	protected float health = 100;

	[SerializeField, Range(0.05f, 0.5f)]
	protected float accuracy = 0.1f;

	[SerializeField]
	private float speed = 2;

	public float Speed { get => speed; set => speed = value; }

	private Vector3 destination;

	public Vector3 Destination { get => destination; set => destination = value; }

	protected Stack<Node> path;

	public abstract UnitType Type { get; set; }

	public static GridPoint GridPosition { get; set; }

	public int strength = 1;
	public int Strength { get => strength; set => strength = value; }

	private void OnEnable()
	{
		TowerBase.targets.Add(this);
	}

	private void OnDisable()
	{
		TowerBase.targets.Remove(this);

		if (TowerBase.targetsWithinRange.Contains(this))
		{
			TowerBase.targetsWithinRange.Remove(this);
		}
	}

	protected virtual void Start()
	{
		SetPath(LevelManager.FinalPath);
	}

	void Update()
	{
		Move();
		CheckDistanceToPlayerBase();
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

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			//Debug.Log(this.name + " died");
			ReturnToPool();
		}
	}

	public virtual void ReturnToPool()
	{
	}
}
