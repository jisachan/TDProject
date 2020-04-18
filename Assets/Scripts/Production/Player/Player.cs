using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	int lives = 20;

	public static Action<UnitBase> reachedPlayerBase;

	public int Lives { get => lives; set => lives = value; }

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

		//edit infinite if later.
		if (lives <= 0)
		{
			//Todo: gameover
			//GameManager.StartNextLevel();
		}
	}
}
