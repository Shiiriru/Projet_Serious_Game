using System;

namespace DialogSystem
{
	public static class CustomVariableTools
	{
		public static bool IsType(this Type variableType, Type checkType)
		{
			return variableType == checkType;
		}

		public static object ToValue(this string value, Type checkType)
		{
			if (checkType == typeof(int))
			{
				var num = 0;
				if (int.TryParse(value, out num))
					return num;
			}
			if (checkType == typeof(float))
			{
				var num = 0f;
				if (float.TryParse(value, out num))
					return num;
			}
			if (checkType == typeof(bool))
			{
				var b = false;
				if (bool.TryParse(value, out b))
					return b;
			}
			if (checkType == typeof(string))
			{
				return value;
			}

			return null;
		}

		public static Type ToType(this VariableType type)
		{
			switch (type)
			{
				case VariableType.Int:
					return typeof(int);
				case VariableType.Float:
					return typeof(float);
				case VariableType.Bool:
					return typeof(bool);
				case VariableType.String:
					return typeof(string);
			}
			return null;
		}

		public static VariableType ToVariableType(this Type type)
		{
			if (type == typeof(int))
				return VariableType.Int;
			else if (type == typeof(float))
				return VariableType.Float;
			else if (type == typeof(bool))
				return VariableType.Bool;
			else if (type == typeof(string))
				return VariableType.String;

			return VariableType.Null;
		}

		public static string ToShortTypeStr(this Type type)
		{
			if (type == typeof(int))
				return "int";
			else if (type == typeof(float))
				return "float";
			else if (type == typeof(bool))
				return "bool";
			else if (type == typeof(string))
				return "string";

			return "";
		}
	}
}