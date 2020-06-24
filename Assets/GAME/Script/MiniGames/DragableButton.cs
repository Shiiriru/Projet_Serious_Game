using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	protected RectTransform rectTransform;
	protected bool isHovering;
	[SerializeField] protected bool disableMove;
	public bool DisableMove { get { return disableMove; } }

	public event System.Action onHoverBegin;
	public event System.Action onHoverEnd;

	protected virtual void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (disableMove)
			return;

		isHovering = true;
		if (onHoverBegin != null)
			onHoverBegin();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (disableMove)
			return;
		isHovering = false;
		if (onHoverEnd != null)
			onHoverEnd();
	}

	private void Update()
	{
		if (!isHovering || disableMove)
			return;

		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = Vector2.Lerp(transform.position, mousePosition, 30 * Time.deltaTime);
	}
}
