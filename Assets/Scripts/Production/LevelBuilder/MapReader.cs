using UnityEngine;
using TowerDefense;
using UnityEngine.SceneManagement;

public static class MapReader
{
	static TextAsset mapText;

	public static string ReadTextMap()
	{
		mapText = Resources.Load(ProjectPaths.RESOURCES_MAP_SETTINGS + "map_"+ GameManager.nextScene.ToString()) as TextAsset;

		return mapText.text;
	}
}
