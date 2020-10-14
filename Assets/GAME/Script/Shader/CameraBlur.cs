using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shaders
{
	[ExecuteInEditMode]
	public class CameraBlur : MonoBehaviour
	{
		enum Quality
		{
			Low,
			Medium,
			High
		}

		[SerializeField] Shader shader;

		[SerializeField] [Range(0, 10f)] float blurSize = 0.5f;
		[SerializeField] bool gaussianBlur = true;
		[SerializeField] [Range(0, 3f)] float standardDeviation = 1f;
		[SerializeField] Quality quality;

		[SerializeField] float blurWaveFrequence;

		float nowBlurSize;
		float nextBlurSize;

		private Material curMaterial;
		Material material
		{
			get
			{
				if (curMaterial == null)
				{
					curMaterial = new Material(shader);
					curMaterial.hideFlags = HideFlags.HideAndDontSave;
				}
				return curMaterial;
			}
		}

		private void Start()
		{
			nextBlurSize = Random.Range(0, blurSize);
		}

		// Update is called once per frame
		void Update()
		{
			if (blurWaveFrequence == 0)
			{
				nowBlurSize = blurSize;
				return;
			}

			if (Mathf.Abs(nowBlurSize - nextBlurSize) > 0.01)
			{
				nowBlurSize += (nextBlurSize > nowBlurSize ? 1 : -1) * blurWaveFrequence * 0.1f * Time.deltaTime;
			}
			else
				nextBlurSize = Random.Range(0, blurSize);
		}

		void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
		{
			if (shader != null)
			{
				material.SetFloat("_BlurSize", nowBlurSize * 0.05f);
				material.SetFloat("_Gauss", gaussianBlur ? 1f : 0);
				material.SetInt("_Samples", (int)quality);
				material.SetFloat("_StandardDeviation", standardDeviation);

				var temporaryTexture = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height);
				Graphics.Blit(sourceTexture, temporaryTexture, material, 0);
				Graphics.Blit(temporaryTexture, destTexture, material, 1);
				RenderTexture.ReleaseTemporary(temporaryTexture);
			}
			else
			{
				Graphics.Blit(sourceTexture, destTexture);
			}
		}

		void OnDisable()
		{
			if (curMaterial != null)
			{
				DestroyImmediate(curMaterial);
			}
		}
	}
}
