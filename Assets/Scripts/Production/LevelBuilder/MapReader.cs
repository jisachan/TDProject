using UnityEngine;
using TowerDefense;

public static class MapReader
{
	public static string ReadTextMap()
	{
		TextAsset mapText = Resources.Load(ProjectPaths.RESOURCES_MAP_SETTINGS + "map_1") as TextAsset;

		return mapText.text;
	}
}
