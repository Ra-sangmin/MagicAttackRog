using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterManager : MonoBehaviour {

	private List<RectTransform> monsterSpawnList = new List<RectTransform>();
	private Monster monster;
	private Character target;

	private int createCount = 5;

	private bool createClear;
	private List<Monster> monsterList = new List<Monster> ();
	public UnityAction monsterAllKillOn;

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
		createClear = false;

		for (int i = 0; i < createCount; i++) 
		{
			yield return new WaitForSeconds (2);

			foreach (var spawnPoint in monsterSpawnList) {
				MonsterSpawnSet (spawnPoint);
			}	
		}

		createClear = true;
	}

	private void MonsterSpawnSet(RectTransform parant)
	{
		Monster monsterObj = Instantiate (Resources.Load<Monster> ("Monster/Book"));	
		monsterObj.transform.SetParantAndReset (parant);

		monsterObj.SetData (this.target , MonsterDieOn);

		monsterList.Add (monsterObj);
	}

	private void MonsterDieOn(Monster monster)
	{
		monsterList.Remove (monster);

		if(createClear && monsterList.Count == 0)
		{
			if(monsterAllKillOn != null)
			{
				monsterAllKillOn ();	
			}
		}


	}

}
