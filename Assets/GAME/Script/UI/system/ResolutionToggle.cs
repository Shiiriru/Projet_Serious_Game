using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ResolutionToggle : MonoBehaviour
{
	[SerializeField] int screenWidth;
	public int ScreenWidth { get { return screenWidth; } }

	public Toggle Toggle { get; private set; }

	// Use this for initialization
	void Awake()
	{
		Toggle = GetComponent<Toggle>();
	}

	public void Init()
	{
		Toggle.onValueChanged.AddListener(ToggleChangeResolution);
	}

	void ToggleChangeResolution(bool on)
	{
		if (on)
			GameManager.SetResolution(screenWidth);
	}

	private void OnDestroy()
	{
		Toggle.onValueChanged.RemoveAllListeners();
	}
}
