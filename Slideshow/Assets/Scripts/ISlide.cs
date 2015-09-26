public interface ISlide
{
	void Initialize(DefaultDelegate onSlideComplete, DefaultDelegate onSlideBack);
	void UpdateSlide(float dt);
	void Unload();
}