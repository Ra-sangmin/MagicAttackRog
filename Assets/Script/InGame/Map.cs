﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	[SerializeField] private List<RectTransform> monsterSpawnList = new List<RectTransform>();
	[SerializeField] private RectTransform portal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<RectTransform> GetMonsterSpawnList()
	{
		return monsterSpawnList;
	}

	public void PortalActiveOn(bool active , bool nextBossOn = false)
	{
		portal.gameObject.SetActive (active);

		if(active)
		{
			AtlasAnimation atlasAnimation = portal.GetComponent<AtlasAnimation> ();

			string imageTag = nextBossOn ? "portal_boss" : "portal_normal";

			atlasAnimation.SetImageTag (imageTag);

			Debug.LogWarning (imageTag);
		}
	}
}
