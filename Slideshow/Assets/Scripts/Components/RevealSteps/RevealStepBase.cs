using System;
using UnityEngine;

public class RevealStepBase : MonoBehaviour, IRevealStep
{
	protected bool revealed = false;
	protected bool revealing = false;

	public RevealStepBase ()
	{
	}

	public virtual void Start()
	{
		// To be overridden.
	}

	public virtual void Reveal()
	{
		// To be overridden.
	}

	public virtual void Reset()
	{
		revealed = false;
		revealing = false;
	}

	public virtual void Update()
	{
		// To be overridden.
	}
}
