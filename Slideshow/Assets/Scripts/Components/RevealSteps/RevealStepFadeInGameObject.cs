using System;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class RevealStepFadeInGameObject : RevealStepBase
{
	public float FadeDuration = 0.5f;
	private float currentFadeTime;
	private Color objColor;

	public RevealStepFadeInGameObject ()
	{
	}

	public override void Start ()
	{
		base.Start ();
	}

	public override void Reveal ()
	{
		base.Reveal ();
		revealing = true;
		gameObject.SetActive (true);
	}

	public override void Reset()
	{
		base.Reset();
		objColor = gameObject.renderer.material.color;
		objColor.a = 0f;
		gameObject.renderer.material.color = objColor;
		gameObject.SetActive (false);
		currentFadeTime = 0f;
	}

	public override void Update ()
	{
		base.Update ();
		if(revealing && !revealed)
		{
			currentFadeTime += Time.deltaTime;
			float pct = currentFadeTime / FadeDuration;
			objColor.a = Mathf.Min(pct, 1f);
			gameObject.renderer.material.color = objColor;
			if(pct >= 1f)
			{
				pct = 1f;
				revealed = true;
			}
		}
	}
}
