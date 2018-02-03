using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;

public static class TransformUtility  {

	
	/// <summary>
	/// Parant Set And Reset To Position,Scale
	/// </summary>
	/// <param name="component"></param>
	/// <param name="parant"></param>
	/// <returns></returns>
	public static Transform SetParantAndReset(this Transform component,Transform parant)
	{
		if (parant != null)
		{
			component.transform.SetParent(parant);
			component.transform.localPosition = Vector3.zero;
			component.transform.localScale = Vector3.one;
		}

		return component;
	}

	public static RectTransform SetParantAndReset(this RectTransform component,RectTransform parant)
	{
		if (parant != null)
		{
			component.transform.SetParent(parant);
			component.transform.localPosition = Vector3.zero;
			component.transform.localScale = Vector3.one;
		}

		return component;
	}
}
