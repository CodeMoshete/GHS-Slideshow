using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class DefaultSlide : BaseSlide
{
	public List<GameObject> RevealStepObjects;

	private Button AdvanceButton;
	private Button BackButton;
	private List<IRevealStep> RevealSteps;

	public DefaultSlide() : base()
	{
	}

	public override void Initialize(DefaultDelegate onComplete, DefaultDelegate onSlideBack)
	{
		base.Initialize (onComplete, onSlideBack);
		AdvanceButton = FindChild ("AdvanceButton").GetComponent<Button>();
		AdvanceButton.onClick.AddListener (AdvancePressed);
		BackButton = FindChild ("GoBackButton").GetComponent<Button>();
		BackButton.onClick.AddListener (BackPressed);

		RevealSteps = new List<IRevealStep>();
		if(RevealStepObjects != null && RevealStepObjects.Count > 0)
		{
			for(int i = 0, count = RevealStepObjects.Count; i < count; i++)
			{
				RevealStepBase component = RevealStepObjects[i].GetComponent<RevealStepBase>();
				component.Reset();
				RevealSteps.Add(component);
			}
		}
	}

	private void AdvancePressed()
	{
		onComplete ();
	}

	private void BackPressed()
	{
		onBack ();
	}

	public override void UpdateSlide (float dt)
	{
		base.UpdateSlide (dt);

		if(Input.GetKeyDown(KeyCode.Space) && RevealSteps != null && RevealSteps.Count > 0)
		{
			RevealSteps[0].Reveal();
			RevealSteps.RemoveAt(0);
		}
	}

	public override void Unload()
	{
		base.Unload ();
		// Intentionally empty
	}
}