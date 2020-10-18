using UnityEngine;
using UnityEditor;

public class MiniGameBase : MonoBehaviour
{
	public bool HasWon { get; protected set; }
	public event System.Action onGameComplete;

	protected bool gameFinished;

	public virtual void Launch()
	{
		HasWon = false;
		gameFinished = false;
		gameObject.SetActive(true);
	}

	public virtual void FnishGame(bool b)
	{
		HasWon = b;
		gameFinished = true;
		OnGameComplete();
	}

	public void OnGameComplete()
	{
		onGameComplete.Invoke();
	}
}