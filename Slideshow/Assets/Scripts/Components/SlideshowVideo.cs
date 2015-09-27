using UnityEngine;
using UnityEngine.UI;

public class SlideshowVideo : MonoBehaviour
{
	public bool AutoPlay;

	private MovieTexture movie;

	public void Start()
	{
		if(gameObject.activeSelf)
		{
			movie = (MovieTexture)GetComponent<RawImage>().texture;

			if(AutoPlay && gameObject.activeSelf)
			{
				Stop();
				Play ();
			}
		}
	}

	public void OnDisable()
	{
		Stop();
	}

	public void Awake()
	{
		movie = (MovieTexture)GetComponent<RawImage>().texture;

		if(AutoPlay)
		{
			Stop();
			Play ();
		}
	}

	public void Play()
	{
		movie.Play();
		if(audio != null)
		{
			audio.Play();
		}
	}

	public void Pause()
	{
		movie.Pause();
		if(audio != null)
		{
			audio.Pause();
		}
	}

	public void Stop()
	{
		movie.Stop();
		if(audio != null)
		{
			audio.Stop();
		}
	}
}