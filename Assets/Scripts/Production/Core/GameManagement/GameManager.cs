using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField, Tooltip("Increase to increase game speed. Decrease to decrease game speed.")]
	float gameSpeed = 1f;

	public static int nextScene = 0;

	static int maxScenes = 0;
	static int currentScene = 0;


	public void Start()
	{
		Time.timeScale = gameSpeed;
		maxScenes = SceneManager.sceneCount;
	}

	public static void StartNextLevel()
	{
		nextScene = currentScene + 1;

		if (currentScene < maxScenes)
		{
			SceneManager.LoadScene(nextScene);
			currentScene++;
		}
		else
		{
			GameFinished();
		}
	}

	public static void GameFinished()
	{
		print("You either lost or won, yay! or aww~.");
		Time.timeScale = 0;
	}
}
