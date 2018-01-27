using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

public class JoyStic : MonoBehaviour , IPointerDownHandler , IPointerUpHandler , IDragHandler {


	[SerializeField] RectTransform movePointImage;

	float maxDistance;

	public ReactiveProperty<MoveEnum> moveEnum = new ReactiveProperty<MoveEnum>(MoveEnum.None);

	// Use this for initialization
	void Start () {

		SetData ();
	}

	private void SetData ()
	{
		maxDistance = 150;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		MovePointPosCheck (eventData);
	}

	public void OnDrag (PointerEventData eventData)
	{
		MovePointPosCheck (eventData);
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		MovePointPosReset (Vector2.zero);
	}


	private void MovePointPosCheck(PointerEventData eventData)
	{
		Vector2 pos = Vector2.zero;

		RectTransform parantRect = movePointImage.parent as RectTransform;

		RectTransformUtility.ScreenPointToLocalPointInRectangle (parantRect, eventData.position, Camera.main,out pos);

		float nowDistance = Vector2.Distance (Vector2.zero, pos);

		if (maxDistance < nowDistance) 
			pos *= maxDistance /nowDistance;
		
		MovePointPosReset (pos);
	}
		
	private void MovePointPosReset(Vector2 pos)
	{
		movePointImage.transform.localPosition = pos;

		if (pos == Vector2.zero) 
		{
			moveEnum.Value = MoveEnum.None;	
		} 
		else if (Mathf.Abs(pos.x) > Mathf.Abs(pos.y))
		{
			moveEnum.Value = pos.x >= 0 ? MoveEnum.Right : MoveEnum.Left;	
		}
		else if (Mathf.Abs(pos.x) < Mathf.Abs(pos.y))
		{
			moveEnum.Value = pos.y >= 0 ? MoveEnum.Up : MoveEnum.Down;	
		}
	}
	 
}

public enum MoveEnum
{
	None,
	Up,
	Down,
	Left,
	Right
}
