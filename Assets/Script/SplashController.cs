using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour {

	[SerializeField] private Image bgImage;

	// Use this for initialization
	void Start () {
		StartCoroutine(SplashDoing());
	}
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
		SceneManager.LoadScene("Title");
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
}