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

		[Header("Other Parameter")]
		[SerializeField] bool isRotate;
		[SerializeField] bool isScale;

		Vector3 originePosition;
		Vector3 originScale;
		Vector3 originRotation;

		// Start is called before the first frame update
		void Awake()
		{
			originePosition = transform.position;
			originScale = transform.localScale;
			originRotation = transform.eulerAngles;

			if (autoStart)
				Shake();
		}

		public void StartShake()
		{
			transform.DOKill();
			Shake();
		}

		public void StopShake()
		{
			transform.DOKill();

			transform.DOMove(originePosition, 0.15f);
			transform.DOScale(originScale, 0.15f);
			transform.DORotate(originRotation, 0.15f);
		}

		void Shake()
		{
			if (isScale)
				transform.DOShakeScale(duration, strength / 110, frequence / 10, Degree, fadeOut: loop ? false : fadeOut);
			if (isRotate)
				transform.DOShakeRotation(duration, strength / 2, frequence, Degree, fadeOut: loop ? false : fadeOut);

			transform.DOShakePosition(duration, strength, frequence, Degree, fadeOut: loop ? false : fadeOut)
				.OnComplete(() =>
				{
					if (loop)
						Shake();
				});
		}
	}
}