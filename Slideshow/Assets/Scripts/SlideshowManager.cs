using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlideshowManager : MonoBehaviour {

	private enum TransitionStatus
	{
		TransitionIn,
		TransitionOut,
		Idle
	}

	private enum TransitionDirection
	{
		Forwards,
		Backwards
	}

	public List<GameObject> Slides;

	public GameObject background;
	public GameObject transitionScreen;
	private CanvasGroup transitionCanvas;

	private GameObject currentSlide;
	private BaseSlide currentSlideScript;
	private int currentSlideIndex;

	private TransitionStatus transitionStatus;
	private TransitionDirection transitionDirection;
	
	private void Start () 
	{
		currentSlideIndex = 0;
		transitionCanvas = transitionScreen.GetComponent<CanvasGroup> ();
		TransitionForwards ();
	}
	
	private void Update () 
	{
		float dt = Time.deltaTime;

		// Handle transitions while they're running
		if(transitionStatus == TransitionStatus.TransitionIn)
		{
			if(transitionCanvas.alpha < 1f)
			{
				transitionCanvas.alpha += dt;
			}
			else
			{
				transitionStatus = TransitionStatus.TransitionOut;
				AdvanceToNextSlide();
			}
		}

		if(transitionStatus == TransitionStatus.TransitionOut)
		{
			if(transitionCanvas.alpha > 0f)
			{
				transitionCanvas.alpha -= dt;
			}
			else
			{
				transitionStatus = TransitionStatus.Idle;
				transitionScreen.SetActive(false);
			}
		}

		transitionCanvas.alpha = Mathf.Clamp (transitionCanvas.alpha, 0f, 1f);

		// Handle current slide update.
		if(currentSlideScript != null)
		{
			currentSlideScript.UpdateSlide(dt);
		}

		// Allow arrow keys to trigger transitions.
		if(Input.GetKeyDown(KeyCode.RightArrow) && transitionStatus == TransitionStatus.Idle)
		{
			TransitionForwards();
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow) && transitionStatus == TransitionStatus.Idle)
		{
			TransitionBackwards();
		}
	}

	private void TransitionBackwards()
	{
		transitionDirection = TransitionDirection.Backwards;
		transitionScreen.SetActive(true);
		transitionStatus = TransitionStatus.TransitionIn;
	}

	private void TransitionForwards()
	{
		transitionDirection = TransitionDirection.Forwards;
		transitionScreen.SetActive(true);
		transitionStatus = TransitionStatus.TransitionIn;
	}

	private void AdvanceToNextSlide()
	{
		if((transitionDirection == TransitionDirection.Forwards && currentSlideIndex < (Slides.Count - 1)) || 
		   (transitionDirection == TransitionDirection.Backwards && currentSlideIndex > 0))
		{
			if(currentSlide != null)
			{
				currentSlideScript.Unload();
				currentSlide.SetActive(false);
				if(transitionDirection == TransitionDirection.Forwards)
				{
					currentSlideIndex++;
				}
				else
				{
					currentSlideIndex--;
				}
			}

			currentSlide = Slides[currentSlideIndex];
			currentSlideScript = currentSlide.GetComponent<BaseSlide>();
			currentSlideScript.Initialize(TransitionForwards, TransitionBackwards);
			currentSlide.SetActive(true);

			background.SetActive(currentSlideScript.EnableBackground);
		}
	}
}
