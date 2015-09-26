using UnityEngine;

public class BaseSlide : MonoBehaviour, ISlide
{
	protected DefaultDelegate onComplete;
	protected DefaultDelegate onBack;

	public BaseSlide()
	{
	}

	public virtual void Initialize(DefaultDelegate onComplete, DefaultDelegate onSlideBack)
	{
		this.onComplete = onComplete;
		onBack = onSlideBack;
	}

	protected GameObject FindChild(string name)
	{
		GameObject returnObj = null;
		int numChildren = gameObject.transform.childCount;
		for(int i = 0; i < numChildren; i++)
		{
			GameObject child = gameObject.transform.GetChild(i).gameObject;
			if(child.name == name)
			{
				returnObj = child;
				break;
			}
		}

		return returnObj;
	}

	public virtual void UpdateSlide(float dt)
	{
		// To be overridden.
	}

	public virtual void Unload()
	{
		// To be overridden.
	}
}