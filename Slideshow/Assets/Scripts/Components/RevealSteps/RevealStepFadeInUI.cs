using System;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class RevealStepFadeInUI : RevealStepBase
{
	public float FadeDuration = 0.5f;
	public bool FadeIn = true;
	public bool FadeOut = false;
	private float currentFadeTime;
	private CanvasGroup canvas;

	public RevealStepFadeInUI ()
	{
	}

	public override void Start ()
	{
		base.Start ();
		canvas = gameObject.GetComponent<CanvasGroup> ();
		if(FadeIn)
		{
			canvas.alpha = 0f;
		}
	}

	public override void Reveal ()
	{
		base.Reveal ();

		if(FadeIn && (!gameObject.activeSelf || canvas.alpha < 1f))
		{
			gameObject.SetActive (true);
			revealing = true;
			hiding = false;
			currentFadeTime = 0f;
		}
		else if(FadeOut && (gameObject.activeSelf || canvas.alpha < 0f))
		{
			revealing = false;
			hiding = true;
			currentFadeTime = 0f;
		}
	}

	public override void Reset()
	{
		base.Reset();
		if(FadeIn)
		{
			if(canvas != null)
				canvas.alpha = 0f;
			gameObject.SetActive (false);
		}
		else
		{
			if(canvas != null)
				canvas.alpha = 1f;
			gameObject.SetActive (true);
		}

		currentFadeTime = 0f;

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
		else if(hiding)
		{
			currentFadeTime += Time.deltaTime;
			float pct = 1 - (currentFadeTime / FadeDuration);
			canvas.alpha = Mathf.Min(pct, 1f);
			if(pct >= 1f)
			{
				pct = 1f;
				revealed = true;
			}
		}
	}
}
