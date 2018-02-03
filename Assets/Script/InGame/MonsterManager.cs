using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {

	private List<RectTransform> monsterSpawnList = new List<RectTransform>();
	private Monster monster;
	private Character target;

	private int createCount = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MonsterSpawnSet(List<RectTransform> monsterSpawnList , Character target)
	{
		this.target = target;
		this.monsterSpawnList = monsterSpawnList;

		StartCoroutine (MonsterCreateReady ());

	}

	IEnumerator MonsterCreateReady()
	{
		for (int i = 0; i < createCount; i++) 
		{
			yield return new WaitForSeconds (2);

			foreach (var spawnPoint in monsterSpawnList) {
				MonsterSpawnSet (spawnPoint);
			}	
		}
	}

	private void MonsterSpawnSet(RectTransform parant)
	{
		Monster monsterObj = Instantiate (Resources.Load<Monster> ("Monster/Book"));	
		monsterObj.transform.SetParantAndReset (parant);

		monsterObj.SetTarget (this.target);
	}

}
