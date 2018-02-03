﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour {

	[SerializeField] MapLoadManager mapLM;
	[SerializeField] Character character;
	[SerializeField] CharacterCamera chaCam;
	[SerializeField] MonsterManager monsterM;


	// Use this for initialization
	void Start () {
		
		MapLoad (0);
	}

	private void MapLoad(int index)
	{
		mapLM.MapChange (index);

		Map currentMap = mapLM.GetCurrentMap ();
		RectTransform mapRect = currentMap.transform as RectTransform;
		chaCam.MapSizeCheck (mapRect.sizeDelta);

		monsterM.MonsterSpawnSet (currentMap.GetMonsterSpawnList(),character);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
