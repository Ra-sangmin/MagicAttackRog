﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobcast.Coffee.UI;

public class Monster : MonoBehaviour {

	[SerializeField] AtlasImage image;

	private Character target;

	private CharacterState characterState;
	private MoveEnum moveEnum;
	private MoveEnum beforeMoveEnum;

	private Delay moveDelay;
	private Delay restDelay;
	private Delay animChangeDelay;

	private float moveSpeed = 0.2f;

	private int animState = 0;
	private Queue<int> attackAnimStateQueue = new Queue<int>();

	// Use this for initialization
	void Start () {
		SetDelayData ();
	}

	private void SetDelayData ()
	{
		moveDelay = new Delay (0.8f);
		restDelay = new Delay (1);
		animChangeDelay = new Delay (0.1f);
	}

	void OnDestroy()
	{
		DelayManager.Instance.RemoveDelayData (moveDelay);
		DelayManager.Instance.RemoveDelayData (restDelay);
		DelayManager.Instance.RemoveDelayData (animChangeDelay);
	}

	public void SetTarget(Character target)
	{
		this.target = target;

		if(this.target != null)
		{
			moveEnum = MoveEnum.Up;	
		}
			
	}

	
	// Update is called once per frame
	void Update () {
		MoveCheck ();
		AnimChangeCheck ();	
	}


	private void MoveCheck ()
	{
		if (target == null || characterState == CharacterState.Die)
			return;

		Vector2 moveValue = Vector2.zero;

		Vector2 resultVector = target.transform.position - transform.position;

		if (Mathf.Abs(resultVector.x) >= Mathf.Abs( resultVector.y)) 
		{
			moveEnum = resultVector.x >= 0 ? MoveEnum.Right : MoveEnum.Left;
		} 
		else 
		{
			moveEnum = resultVector.y >= 0 ? MoveEnum.Up : MoveEnum.Down;
		}


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

		if (!moveDelay.delayOn) 
		{
			transform.Translate (moveValue * Time.deltaTime * 20 * moveSpeed);	
		} 
	}


	private void AnimChangeCheck ()
	{
		if(characterState == CharacterState.Die)
		{
			return;	
		}
		
		if (attackAnimStateQueue.Count != 0) 
		{
			animState = attackAnimStateQueue.Dequeue();
			AnimReset ();

			bool necromancy = false;

			if(animState % 2 == 0 || necromancy)
			{
				Debug.LogWarning ("Attack");
			}

			if(attackAnimStateQueue.Count == 0)
			{
				characterState = moveEnum == MoveEnum.None ? CharacterState.None : CharacterState.Move;
			}
		} 
		else 
		{
			if(!animChangeDelay.delayOn)
			{
				NextImageReset ();
				animChangeDelay.SetDelay ();
			}
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
		string monsterName = "book";
		string chaStateName = GetChaStateAnimName ();
		string moveName = moveEnum == MoveEnum.None ? GetMoveName(beforeMoveEnum) : GetMoveName(moveEnum);
		int animStateNum = animState + 1;

		string animName = string.Format ("{0}_{1}_{2}", monsterName,chaStateName, animStateNum);

		image.spriteName = animName;
	}

	private string GetChaStateAnimName()
	{
		string chaStateName = "";
		switch(characterState)
		{
		case CharacterState.None:
			chaStateName = "rest";
			break;
		case CharacterState.Move:
			chaStateName = "walk";
			break;
		case CharacterState.Attack:
			chaStateName = "attack";
			break;
		case CharacterState.MoveAttack:
			chaStateName = "move attack";
			break;
		}
		return chaStateName;
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

	public void DamageOn()
	{
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;

		characterState = CharacterState.Die;

		StartCoroutine (DieEffect ());
	}

	IEnumerator DieEffect()
	{
		image.CrossFadeAlpha (0, 1, false);

		yield return new WaitForSeconds (1);

		Destroy (gameObject);
	}

}

