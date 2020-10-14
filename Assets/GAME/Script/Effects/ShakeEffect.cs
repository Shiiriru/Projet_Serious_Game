using DG.Tweening;
using UnityEngine;

namespace effect
{
	public class ShakeEffect : MonoBehaviour
	{
		[SerializeField] bool autoStart = true;

		[SerializeField] float duration = 1f;
		[SerializeField] float strength = 2f;
		[SerializeField] int frequence = 1;
		[SerializeField] float Degree = 360;

		[SerializeField] bool fadeOut;
		[SerializeField] bool loop;

		// Start is called before the first frame update
		void Start()
		{
			if (autoStart)
				Shake();
		}

		void Shake()
		{
			transform.DOShakePosition(duration, strength, frequence, Degree, fadeOut: loop ? false : fadeOut)
				.OnComplete(() =>
				{
					if (loop)
						Shake();
				});
		}
	}
}