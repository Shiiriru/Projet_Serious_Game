using System;
using System.Collections.Generic;

namespace DialogSystem
{
	[Serializable]
	public class VariableObject : VariableBase
	{
		public string name;

		[NonSerialized]
		public int valueIndex = -1;
	}

	[Serializable]
	public class VariableBase
	{
		public VariableType vType;
		public string valueStr;
		public object value { get { return valueStr.ToValue(vType.ToType()); } }
	}
}
