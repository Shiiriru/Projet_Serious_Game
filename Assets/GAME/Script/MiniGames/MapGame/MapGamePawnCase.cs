using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MapGamePawnCase : MonoBehaviour
{
	public MapGamePawn targetPawn { get; private set; }

	public void fillCase(MapGamePawn pawn)
	{
		targetPawn = pawn;
	}
}
