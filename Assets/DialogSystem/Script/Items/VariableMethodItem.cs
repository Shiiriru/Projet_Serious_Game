using System;

namespace DialogSystem
{
	[Serializable]
	public class VariableMethodItem
	{
		public VariableType vType;
		public VariableObject variable = new VariableObject();
		public MethodObject method = new MethodObject();

		[NonSerialized]
		public int valueIndex = -1;
	}
}