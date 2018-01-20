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

	/*
	IEnumerator SplashDoing()
	{
		Color targetColor = bgImage.color;
		targetColor.a = 0;
		bgImage.color = targetColor;
		yield return new WaitForSeconds(0.5f);
		float duration = 3.0f;
		SetAlpha(1, duration, Ease.InFlash);
		yield return new WaitForSeconds(duration + 1.5f);
		duration = 1.5f;
		SetAlpha(0, duration, Ease.Linear);
		yield return new WaitForSeconds(duration + 0.5f);
	}

	private void SetAlpha(float targetAlpha, float duration , Ease easeStyle)
	{
		DOTween.ToAlpha(
			() => bgImage.color,
			color => bgImage.color = color,
			targetAlpha,
			duration
		)
			.SetEase(easeStyle);
	}
	*/
	public void GoMainOn()
	{
		SceneManager.LoadScene("Main");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
