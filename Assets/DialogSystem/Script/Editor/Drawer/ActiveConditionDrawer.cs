//using UnityEngine;
//using UnityEditor;

//namespace DialogSystem
//{
//	[CustomPropertyDrawer(typeof(VariableObject))]
//	public class ActiveConditionDrawer : PropertyDrawer
//	{
//		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//		{
//			//base.OnGUI(position, property, label);

//			EditorGUI.BeginProperty(position, label, property);

//			// Draw label
//			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//			// Don't make child fields be indented
//			var indent = EditorGUI.indentLevel;
//			EditorGUI.indentLevel = 1;

//			// Calculate rects
//			var keyRect = new Rect(position.x, position.y, EditorGUIUtility.currentViewWidth * 0.3f, 15);
//			var valueRect = new Rect(position.x + EditorGUIUtility.currentViewWidth * 0.3f, position.y, EditorGUIUtility.currentViewWidth * 0.3f, 15);

//			EditorGUI.PropertyField(keyRect, property.FindPropertyRelative("key"), GUIContent.none);
//			EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);

//			// Set indent back to what it was
//			EditorGUI.indentLevel = indent;

//			EditorGUI.EndProperty();
//		}

//		//public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//		//{
//		//	return 30.0f;
//		//}
//	}
//}