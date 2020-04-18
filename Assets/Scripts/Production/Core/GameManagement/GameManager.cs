using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	static int maxScenes = 0;
	static int currentScene = 0;
	public static int nextScene = 0;

	[SerializeField]
	float gameSpeed = 1f;

	private void Awake()
	{
		Time.timeScale = gameSpeed;
	}

	public void Start()
	{
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
	//int i = 1;
	//int temp = 2;
	//bool slow = false;

	//// Start is called before the first frame update
	//void Start()
	//{
	//    // 1 -> 3 -> 2 -> 4
	//    //Debug.Log("1");
	//    StartCoroutine(TestCoroutine());
	//    Debug.Log(slow + "after return");
	//    //Debug.Log("2");
	//}

	//private IEnumerator TestCoroutine()
	//{
	//    if (slow == false)
	//    {
	//        Debug.Log("slow is false");
	//        temp = i;
	//        slow = true;
	//        Debug.Log("slow is true");
	//        //Debug.Log("3");
	//        yield return new WaitForSeconds(2f);
	//        //Debug.Log("4");
	//        i = temp;
	//        slow = false;
	//        Debug.Log("slow is false again");
	//    }
	//}
}
