//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArrayManager))]
public class ArrayManagerEditor : Editor {
	
	[MenuItem("Component/Modifiers/Array")]
	static void Array(){
		if(Selection.activeGameObject != null)
			Selection.activeGameObject.AddComponent(typeof(ArrayManager));
	}
	private int typeArray = 0;
	private string[] arrayName = new string[]{"Linear", "Curve", "Object"};
	public override void OnInspectorGUI()
	{
		
		//Отрисовка инспектора по умолчанию
		DrawDefaultInspector();
		
		ArrayManager arrManager = target as ArrayManager;
		EditorGUIUtility.LookLikeControls();
		EditorGUILayout.BeginHorizontal();
		typeArray = EditorGUILayout.Popup("Type", typeArray, arrayName, EditorStyles.popup);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Separator();
		if(GUILayout.Button("Add Array"))
			arrManager.NewArray(typeArray);
		EditorGUILayout.Separator();
		EditorGUILayout.EndHorizontal();
	}
}
