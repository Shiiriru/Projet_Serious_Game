using System;

namespace DialogSystem
{
	[Serializable]
	public class MethodObject
	{
		public string fullMethod;
		public string name;
		public bool hasParameter;

		public VariableBase parameter = new VariableBase();
	}
}