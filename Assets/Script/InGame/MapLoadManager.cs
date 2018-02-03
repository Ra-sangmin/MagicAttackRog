using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class MapLoadManager : MonoBehaviour {
	
	private Dictionary<int,Map> mapDic = new Dictionary<int, Map>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MapChange(int index)
	{
		GameDataManager.Instance.mapIndex = index;

		if(mapDic.ContainsKey(index))
		{
			foreach (var mapObj in mapDic) {
				mapObj.Value.gameObject.SetActive (false);
			}

			mapDic [index].gameObject.SetActive (true);
		}
		else
		{
			Map mapObj = Instantiate (Resources.Load<Map> ("Map/Map_" + index));	
			mapObj.transform.parent = transform;
			mapObj.transform.localPosition = Vector3.zero;
			mapObj.transform.localScale = Vector3.one;

			mapDic.Add (index, mapObj);
		}
	}

	public Map GetCurrentMap()
	{
		return mapDic [GameDataManager.Instance.mapIndex];
	}

}
