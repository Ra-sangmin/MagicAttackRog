using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	[SerializeField] private Text text;

	private bool alphaOn;

	// Use this for initialization
	void Start () {

		//Color targetColor = text.color;
		//targetColor.a = 0;

		SetTextAlpha ();
		
	}

	private void SetTextAlpha()
	{
		alphaOn = !alphaOn;
		
		float targetAlpha = alphaOn ? 0.1f : 1;

		DOTween.ToAlpha (
			() => text.color,
			color => text.color = color,
			targetAlpha,
			1
		).OnComplete(() => SetTextAlpha());
	}

	public void GoMainOn()
	{
		SceneManager.LoadScene("Main");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
