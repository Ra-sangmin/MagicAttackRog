using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour {

	[SerializeField] MapLoadManager mapLM;
	[SerializeField] CharacterCamera chaCam;

	// Use this for initialization
	void Start () {
		
		MapLoad (0);
	}

	private void MapLoad(int index)
	{
		mapLM.MapChange (index);
		chaCam.MapSizeCheck (mapLM.GetCurrentMap ().sizeDelta);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
