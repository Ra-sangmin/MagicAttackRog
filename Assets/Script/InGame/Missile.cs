using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobcast.Coffee.UI;

public class Missile : MonoBehaviour {

	[SerializeField] AtlasImage image;

	private MoveEnum moveEnum;
	private float moveSpeed = 3f;

	private float moveMaxDelay = 0.8f;
	private float moveCurrentDelay = 0f;

	private float animChangeMaxDelay = 0.1f;
	private float animChangeCurrentDelay = 0.0f;
	private int animState = 0;

	// Use this for initialization
	void Start () {
		
	}

	public void SetData(MoveEnum moveEnum)
	{
		this.moveEnum = moveEnum;

		float rotY = this.moveEnum == MoveEnum.Right ? 180 : 0;
		image.transform.localRotation = Quaternion.Euler (new Vector3 (0, rotY, 0));
	}

	private void MoveCheck ()
	{
		Vector2 moveValue = Vector2.zero;

		switch(moveEnum)
		{	
			case MoveEnum.Left: 
				moveValue = new Vector2 (-1, 0);
				break;
			case MoveEnum.Right:
				moveValue = new Vector2 (-1, 0);
				break;
			case MoveEnum.Up: 
				moveValue = new Vector2 (0, 1);
				break;
			case MoveEnum.Down:
			default:
				moveValue = new Vector2 (0, -1);
				break;
		}

		transform.Translate (moveValue * Time.deltaTime * 20 * moveSpeed);
	}

	// Update is called once per frame
	void Update () {
		MoveCheck ();
		AnimChangeCheck ();
	}

	private void AnimChangeCheck ()
	{
		animChangeCurrentDelay -= Time.deltaTime;

		if(animChangeCurrentDelay < 0)
		{
			animChangeCurrentDelay = animChangeMaxDelay;
			NextImageReset ();
		}
	}
	private void NextImageReset()
	{
		animState++;

		if(animState == 4)
			animState = 0;

		AnimReset ();
	}

	private void AnimReset()
	{
		string headerName = "energybolt";
		string moveName = GetMoveName(moveEnum);
		int animStateNum = animState + 1;

		string animName = string.Format ("{0} - {1}{2}", headerName, moveName, animStateNum);

		image.spriteName = animName;
	}

	private string GetMoveName(MoveEnum moveEnumValue)
	{
		string moveName = "";
		switch(moveEnumValue)
		{
		case MoveEnum.None: 
		case MoveEnum.Down: 
			moveName = "front";
			break;
		case MoveEnum.Up: 
			moveName = "behind";
			break;
		case MoveEnum.Left:
		case MoveEnum.Right:
			moveName = "left";
			break;
		}

		return moveName;
	}
}
