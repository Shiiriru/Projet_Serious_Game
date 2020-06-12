using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlackJackCard : MonoBehaviour
{
	public RectTransform RectTransform { get; private set; }
	Image image;

	[SerializeField] Sprite spriteBack;
	[SerializeField] Sprite[] cardList;
	Sprite currentSprite;

	public int Point { get; private set; }
	public int CurrentPoint { get { return Mathf.Clamp(Point, 1, 10); } }

	// Start is called before the first frame update
	void Awake()
	{
		RectTransform = GetComponent<RectTransform>();
		image = GetComponent<Image>();
	}

	public void Init()
	{
		var num = Random.Range(1, cardList.Length + 1);

		Point = num;
		currentSprite = cardList[Point - 1];
		Show(true);
	}

	public void Show(bool isShow)
	{
		image.sprite = isShow ? currentSprite : spriteBack;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
