using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickGameStart()
	{
		SceneManager.LoadScene("InGame");
	}

	public void OnClickGoMainStart()
	{
		SceneManager.LoadScene("Main");
	}

}
