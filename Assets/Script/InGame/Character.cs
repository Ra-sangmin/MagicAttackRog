using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobcast.Coffee.UI;
using UniRx;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {
	
	[SerializeField] Camera cam;

	[SerializeField] AtlasImage image;
	[SerializeField] JoyStic joyStic;

	private MoveEnum moveEnum;
	private MoveEnum beforeMoveEnum;
	public CharacterState characterState;

	private float moveSpeed = 1f;

	private Delay moveDelay;
	private Delay restDelay;
	private Delay animChangeDelay;

	private int animState = 0;
	private Queue<int> attackAnimStateQueue = new Queue<int>();
	[SerializeField] RectTransform missileParant;
	[SerializeField] RectTransform missilePrefabs;


	// Use this for initialization
	void Start () {

		SetEvent ();
		SetDelayData ();
	}

	private void SetEvent ()
	{
		joyStic.moveEnum.Subscribe (CharacterMoveOn)
			.AddTo(gameObject);
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

	private void CharacterMoveOn(MoveEnum moveEnum)
	{
		if(this.moveEnum == moveEnum)
			return;
		
		if(moveEnum == MoveEnum.None)
		{
			beforeMoveEnum = this.moveEnum;
		}
		this.moveEnum = moveEnum;

		animState = 0;

		bool rightOn =	moveEnum == MoveEnum.Right ||
		               	moveEnum == MoveEnum.None && beforeMoveEnum == MoveEnum.Right;

		float rotY = rightOn ? 180 : 0;
		image.transform.localRotation = Quaternion.Euler (new Vector3 (0, rotY, 0));

		AnimReset ();	

		characterState = moveEnum == MoveEnum.None ? CharacterState.None : CharacterState.Move;

	}
	
	// Update is called once per frame
	void Update () {
		InputCheck ();
		MoveCheck ();
		AnimChangeCheck ();
	}

	void InputCheck ()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			OnClickAttackBtn ();
		}

		if(Input.GetKey(KeyCode.UpArrow))
		{
			CharacterMoveOn (MoveEnum.Up);
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			CharacterMoveOn (MoveEnum.Down);
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			CharacterMoveOn (MoveEnum.Left);
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			CharacterMoveOn (MoveEnum.Right);
		}

		if(Input.GetKeyUp(KeyCode.RightArrow) ||
			Input.GetKeyUp(KeyCode.LeftArrow)||
			Input.GetKeyUp(KeyCode.UpArrow)||
			Input.GetKeyUp(KeyCode.DownArrow))
		{
			if(!Input.GetKey(KeyCode.RightArrow)||
				!Input.GetKey(KeyCode.LeftArrow)||
				!Input.GetKey(KeyCode.UpArrow)||
				!Input.GetKey(KeyCode.DownArrow))
			{
				CharacterMoveOn (MoveEnum.None);	
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("Main");
		}
	}

	private void RestDeleyCheck ()
	{
		if (characterState == CharacterState.None ||
			characterState == CharacterState.Move)
			return;

		if (!restDelay.delayOn) 
			characterState = CharacterState.None;
		
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

		if (!moveDelay.delayOn) 
		{
			transform.Translate (moveValue * Time.deltaTime * 20 * moveSpeed);	
		} 
	}


	private void AnimChangeCheck ()
	{
		if (attackAnimStateQueue.Count != 0) 
		{
			animState = attackAnimStateQueue.Dequeue();
			AnimReset ();

			bool necromancy = false;

			if(animState % 2 == 0 || necromancy)
			{
				MissileCreate ();
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
		string chaStateName = GetChaStateAnimName ();
		string moveName = moveEnum == MoveEnum.None ? GetMoveName(beforeMoveEnum) : GetMoveName(moveEnum);
		int animStateNum = animState + 1;
	
		string animName = string.Format ("{0} - {1}{2}", chaStateName, moveName, animStateNum);

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

	private void MissileCreate ()
	{
		RectTransform missileObj = Instantiate(missilePrefabs, missileParant); 
		Missile missile = missileObj.GetComponent<Missile> ();
		missile.transform.position = transform.position;

		MoveEnum setMoveEnum = moveEnum == MoveEnum.None ? beforeMoveEnum : moveEnum;
		missile.SetData (setMoveEnum);
	}

	public void OnClickAttackBtn()
	{
		characterState = characterState == CharacterState.Move ? CharacterState.MoveAttack : CharacterState.Attack;
		int ranState = Random.Range (0, 2) * 2;

		attackAnimStateQueue.Enqueue (ranState);
		attackAnimStateQueue.Enqueue (ranState+1);

		moveDelay.SetDelay ();
	}

}

public enum CharacterState
{
	None,
	Move,
	Attack,
	MoveAttack
}