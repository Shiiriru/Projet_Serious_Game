using UnityEngine;
using UnityEditor;

public class MiniGameBase : MonoBehaviour
{
	public event System.Action onGameComplete;

	public virtual void GameComplete()
	{
		if (onGameComplete != null)
			onGameComplete();
	}
}