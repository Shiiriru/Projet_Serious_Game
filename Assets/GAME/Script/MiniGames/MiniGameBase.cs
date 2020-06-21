using UnityEngine;
using UnityEditor;

public class MiniGameBase : MonoBehaviour
{
	public event System.Action onGameComplete;

	protected bool gameFinished;

	public virtual void Launch()
	{
		gameFinished = false;
		gameObject.SetActive(true);
	}

	public virtual void FnishGame()
	{
		gameFinished = true;
		if (onGameComplete != null)
			onGameComplete();
	}

	public void OnGameComplete()
	{
		if (onGameComplete != null)
			onGameComplete();
	}
}