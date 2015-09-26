using System;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class RevealStepFadeInUI : RevealStepBase
{
	public float FadeDuration = 0.5f;
	private float currentFadeTime;
	private CanvasGroup canvas;

	public RevealStepFadeInUI ()
	{
	}

	public override void Start ()
	{
		base.Start ();
		canvas = gameObject.GetComponent<CanvasGroup> ();
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
		if(canvas != null)
			canvas.alpha = 0f;

		currentFadeTime = 0f;
		gameObject.SetActive (false);
	}

	public override void Update ()
	{
		base.Update ();
		if(revealing && !revealed)
		{
			currentFadeTime += Time.deltaTime;
			float pct = currentFadeTime / FadeDuration;
			canvas.alpha = Mathf.Min(pct, 1f);
			if(pct >= 1f)
			{
				pct = 1f;
				revealed = true;
			}
		}
	}
}
