using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DatePanelInfo", menuName = "DatePanel Info", order = 1)]
public class DatePanelInfosObject : ScriptableObject
{
	public string date;
	[TextArea] public string place;
	public int soldatCount;
}
