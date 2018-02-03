using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay {

	public bool delayOn{ get; private set;}
	private float maxDelay = 1f;
	private float currentDelay = 0.0f;

	public Delay(){}
	public Delay(float maxDelay)
	{
		this.maxDelay = maxDelay;
		DelayManager.Instance.AddDelayData (this);
	}

	public void SetDelay()
	{
		this.currentDelay = maxDelay;
		delayOn = true;
	}

	public void CheckDelay()
	{
		if(delayOn == false)
			return;

		this.currentDelay -= Time.deltaTime;

		if(this.currentDelay <= 0)
		{
			delayOn = false;	
		}
	}

}
