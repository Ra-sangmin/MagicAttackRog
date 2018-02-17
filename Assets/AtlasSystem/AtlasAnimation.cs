using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobcast.Coffee.UI;
using System.Linq;

public class AtlasAnimation : MonoBehaviour {

	private AtlasImage image;
	public string imageTag;
	private List<string> imageNameList = new List<string>();
	private int animState;

	public float animChangeDelay = 0.1f;
	private float currentAnimDelay = 0;

	// Use this for initialization
	void Start () {

		if(image == null)
		{
			image = GetComponent<AtlasImage> ();
		}

		if(imageTag != string.Empty)
		{
			SetImageTag (imageTag);	
		}

	}

	public void SetImageTag(string imageTag)
	{
		if(image == null)
			return;

		List<string> newNameList = new List<string> ();

		char[] imageTagChar = imageTag.ToCharArray(); 

		foreach (var sprite in image.atlas.sprites) {

			if(sprite.name.Length > imageTag.Length)
			{
				
				char[] checkText = sprite.name.Remove (imageTag.Length).ToCharArray(); 

				bool allMatch = true;

				for (int i = 0; i < checkText.Length; i++) {

					if(checkText[i] != imageTagChar[i])
					{
						allMatch = false;
						break;
					}

				}

				if(allMatch)
				{
					newNameList.Add (sprite.name);
				}

			}

		}

		if(newNameList.Count == 0)
			return;


		newNameList.Sort ();
		imageNameList = newNameList;

		animState = 0;
		currentAnimDelay = animChangeDelay;
		AnimReset ();
	}
	
	// Update is called once per frame
	void Update () {
		NextImageReset ();
	}

	private void NextImageReset()
	{
		if(imageNameList.Count == 0)
			return;

		currentAnimDelay -= Time.deltaTime;

		if(currentAnimDelay < 0)
		{
			currentAnimDelay = animChangeDelay;
				
			animState++;

			if(animState == imageNameList.Count)
				animState = 0;

			AnimReset ();
		}

	}

	private void AnimReset()
	{
		string animName = imageNameList [animState];
		image.spriteName = animName;
	}

}
