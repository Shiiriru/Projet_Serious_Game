using UnityEngine;
using UnityEngine.UI;

public class UIOption : MonoBehaviour
{
	[SerializeField] ResolutionToggle[] resolutionToggles;
	[SerializeField] Slider volumeSlider;

	void Start()
	{
		foreach(var t in resolutionToggles)
		{
			if (t.ScreenWidth == GameManager.optionData.screenWidth)
				t.Toggle.isOn = true;
		}

		foreach (var t in resolutionToggles)
			t.Init();
	}
}

