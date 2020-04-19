using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("Number of hits the enemy base can take before it's Game Over.")]
	int lives = 20;

	public int Lives { get => lives; set => lives = value; }

	public static Action<UnitBase> reachedPlayerBase;
	
	public void Start()
	{
		reachedPlayerBase += UponReachingPlayerBase;
	}

	private void OnDestroy()
	{
		reachedPlayerBase -= UponReachingPlayerBase;
	}

	private void UponReachingPlayerBase(UnitBase unit)
	{
		LoseLives(unit.Strength);
		unit.ReturnToPool();
	}

	public void LoseLives(int livesLost)
	{
		lives -= livesLost;
		Debug.Log("Current lives: " + lives);
		if (lives <= 0)
		{
			GameManager.GameFinished();
		}
	}
}