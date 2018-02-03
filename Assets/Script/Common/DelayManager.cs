using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class DelayManager : MonoBehaviour {

	#region sington
	private static DelayManager ins;
	public static DelayManager Instance
	{
		get
		{
			if(ins == null)
			{
				DelayManager haveData = GameObject.FindObjectOfType<DelayManager> ();

				if (haveData) {
					ins = haveData;
				} 
				else 
				{
					GameObject obj = new GameObject ("DelayManager");
					ins = obj.AddComponent<DelayManager> ();
					GameObject.DontDestroyOnLoad (obj);
				}
			}

			return ins;
		}
	}
	#endregion

	List<Delay> delayList = new List<Delay>();

	// Use this for initialization
	void Start () {
		
	}

	public void AddDelayData(Delay delayData)
	{
		delayList.Add (delayData);
	}

	public void RemoveDelayData(Delay delayData)
	{
		delayList.Remove (delayData);
	}

	// Update is called once per frame
	void Update () {
		DelayCheck ();
	}

	private void DelayCheck ()
	{
		delayList.ForEach (delayData => delayData.CheckDelay ());		
	}


}
