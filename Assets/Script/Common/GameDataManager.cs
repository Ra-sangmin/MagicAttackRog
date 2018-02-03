using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager {
	
	#region sington
	private static GameDataManager ins;
	public static GameDataManager Instance
	{
		get
		{
			if(ins == null)
				ins = new GameDataManager ();
			
			return ins;
		}
	}
	private GameDataManager(){}
	#endregion

	public int mapIndex = 0;

}
