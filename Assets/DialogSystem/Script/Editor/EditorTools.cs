using UnityEngine;
using UnityEditor;

namespace DialogSystem
{
	public static class EditorTools
	{
		public static Texture2D MakeBoxTexture(int width, int height, Color col)
		{
			Color[] pix = new Color[width * height];
			for (int i = 0; i < pix.Length; ++i)
			{
				pix[i] = col;
			}
			Texture2D result = new Texture2D(width, height);
			result.SetPixels(pix);
			result.Apply();
			return result;
		}

		public static bool IsOdd(this int value)
		{
			return value % 2 != 0;
		}
	}
}
