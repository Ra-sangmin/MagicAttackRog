using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobcast.Coffee.UI;
using UniRx;

public class Character : MonoBehaviour {

	[SerializeField] AtlasImage image;
	[SerializeField] JoyStic joyStic;

	private MoveEnum moveEnum;

	private float moveSpeed = 1f;

	private float animChangeMaxDelay = 0.4f;
	private float animChangeCurrentDelay = 0.0f;

	int animState = 0;

	// Use this for initialization
	void Start () {

		SetEvent ();
	}

	private void SetEvent ()
	{
		joyStic.moveEnum.Subscribe (CharacterMoveOn);
	}

	private void CharacterMoveOn(MoveEnum moveEnum)
	{
		this.moveEnum = moveEnum;

		string moveImage = "walk - front1";
		animState = 0;
		float rotY = moveEnum == MoveEnum.Right ? 180 : 0;

		AnimReset ();	

		image.transform.localRotation = Quaternion.Euler (new Vector3 (0, rotY, 0));

	}

	private string GetAnimName(MoveEnum moveEnum)
	{
		string animName = "";
		switch(moveEnum)
		{
		case MoveEnum.Down: 
		case MoveEnum.None: 
			animName = "walk - front";
			break;
		case MoveEnum.Up: 
			animName = "walk - behind";
			break;
		case MoveEnum.Left:
		case MoveEnum.Right:
			animName = "walk - left";
			break;
		}

		return animName;
	}
	
	// Update is called once per frame
	void Update () {
		MoveCheck ();
		AnimChangeCheck ();
	}

	private void MoveCheck ()
	{
		if (moveEnum == MoveEnum.None)
			return;

		Vector2 moveValue = Vector2.zero;

		switch(moveEnum)
		{
			case MoveEnum.Down: 
				moveValue = new Vector2 (0, -1);
				break;
			case MoveEnum.Up: 
				moveValue = new Vector2 (0, 1);
				break;
			case MoveEnum.Left: 
				moveValue = new Vector2 (-1, 0);
				break;
			case MoveEnum.Right:
				moveValue = new Vector2 (1, 0);
				break;
		}

		transform.Translate (moveValue * Time.deltaTime * 20 * moveSpeed);
	}

	private void AnimChangeCheck ()
	{
		if (moveEnum == MoveEnum.None)
			return;

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
		{
			animState = 0;
		}
		AnimReset ();
	}

	private void AnimReset()
	{
		string animName = GetAnimName (moveEnum) + (animState + 1);
		image.spriteName = animName;
	}
}
