using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	[SerializeField] private List<RectTransform> monsterSpawnList = new List<RectTransform>();

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
}
