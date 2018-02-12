using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour {

	[SerializeField] MapLoadManager mapLM;
	[SerializeField] Character character;
	[SerializeField] CharacterCamera chaCam;
	[SerializeField] MonsterManager monsterM;

	int mapIndex = 0;

	// Use this for initialization
	void Start () {

		character.portalOn = NextMapCheck; 
		MapLoad (mapIndex);
	}

	private void MapLoad(int index)
	{
		mapLM.MapChange (index);

		Map currentMap = mapLM.GetCurrentMap ();
		RectTransform mapRect = currentMap.transform as RectTransform;
		chaCam.MapSizeCheck (mapRect.sizeDelta);
		currentMap.PortalActiveOn (false);

		monsterM.MonsterSpawnSet (currentMap.GetMonsterSpawnList(),character);
		monsterM.monsterAllKillOn = MonsterAllKillOn;
	}

	private void MonsterAllKillOn()
	{
		Map currentMap = mapLM.GetCurrentMap ();
		currentMap.PortalActiveOn (true);

	}

	private void NextMapCheck()
	{
		mapIndex++;

		if(mapIndex == 2)
			mapIndex = 0;
		
		MapLoad (mapIndex);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
