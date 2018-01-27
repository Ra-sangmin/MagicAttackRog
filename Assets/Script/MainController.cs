using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickDictionaryBtn()
	{
		
	}

	public void OnClickOptionBtn()
	{

	}

	public void OnClickNewGameBtn()
	{
		SceneManager.LoadScene("SelectCharacter");
	}

	public void OnClickContinueBtn()
	{
		SceneManager.LoadScene("SelectCharacter");
	}

	public void OnClickShopBtn()
	{

	}

	public void OnClickExitBtn()
	{
		Application.Quit ();
	}


}
