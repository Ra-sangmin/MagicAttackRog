using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CharacterCamera : MonoBehaviour {

	[SerializeField] Camera cam;
	[SerializeField] Character character;
	private Vector3 chaBeforePos;
	private Vector2 mapSize;
	private Vector2 halfCamArea;

	// Use this for initialization
	void Start () {

		SetEvent ();
	}

	private void SetEvent ()
	{
		this.UpdateAsObservable ()
			.Where(_=> cam != null && character != null)
			.DistinctUntilChanged (checkValue => cam.orthographicSize)
			.Subscribe (_=>ChangeCamArea())
			.AddTo(gameObject);

		this.UpdateAsObservable ()
			.Where (_ => character != null)
			.Select (checkValue => character.transform.localPosition)
			.DistinctUntilChanged ()
			.Where(pos => chaBeforePos != pos)
			.Subscribe (pos =>CameraMove(pos))
			.AddTo(gameObject);
	}


	public void CharacterSet(Character character)
	{
		this.character = character;
	}

	public void MapSizeCheck(Vector2 mapSize)
	{
		this.mapSize = mapSize;
	}

	private void ChangeCamArea()
	{
		float camXOneperSize = 33.5f;
		float camYOneperSize = 19f;

		float xMaxValue = cam.orthographicSize * camXOneperSize;
		float yMaxValue = cam.orthographicSize * camYOneperSize;

		if(this.mapSize.x < xMaxValue || this.mapSize.y < yMaxValue)
		{
			cam.orthographicSize = Mathf.Min ((int)(this.mapSize.x / camXOneperSize), (int)(this.mapSize.y / camYOneperSize));

			xMaxValue = cam.orthographicSize * camXOneperSize;
			yMaxValue = cam.orthographicSize * camYOneperSize;
		}

		float canMoveXValue = mapSize.x - xMaxValue;
		float canMoveyValue = mapSize.y - yMaxValue;

		halfCamArea = new Vector2 (canMoveXValue / 2, canMoveyValue / 2);

		CameraMove (chaBeforePos);
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void CameraMove (Vector2 chaPos)
	{
		chaBeforePos = chaPos;

		float xPos = Mathf.Clamp (chaPos.x, -halfCamArea.x, halfCamArea.x);
		float yPos = Mathf.Clamp (chaPos.y, -halfCamArea.y, halfCamArea.y);

		cam.transform.localPosition = new Vector3 (xPos, yPos, -10);

	}




}
