//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectArray))]
public class ObjectArrayEditor : Editor {
	
	private GameObject array;
	private GameObject go = null;
	
	#region Переменные изменений
	private GameObject _object1 = null;
	private Vector3 _posobject1 = Vector3.zero;
	private Quaternion _rotobject1 = Quaternion.identity;
	private int _count1 = 0;
	private bool _alloworiginal1 = true;
	private bool _useoffset1 = true;
	private bool _userot1 = true;
	private bool _usescale1 = false;
	#endregion
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();
		
		ObjectArray arrObj = target as ObjectArray;
		if(arrObj.Object != null && (arrObj.UseOffset || arrObj.UseRotation || arrObj.UseScale)){
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Separator();
			if(GUILayout.Button("Create Array"))
				ObjectArray();
			EditorGUILayout.Separator();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Separator();
			if(GUILayout.Button("Apply"))
				DestroyImmediate(arrObj.GetComponent<ObjectArray>());
			EditorGUILayout.Separator();
			if(GUILayout.Button("Cancel")){
				array = GameObject.Find("Array Object " + arrObj.gameObject.name);
				if(array != null)
					DestroyImmediate(array.gameObject);
				DestroyImmediate(arrObj.GetComponent<ObjectArray>());
			}
			EditorGUILayout.Separator();
			EditorGUILayout.EndHorizontal();
		}
		if(arrObj.Count == 0)
			arrObj.Count = 1;
	}
	void OnSceneGUI(){
		ObjectArray arrObj = (ObjectArray) target as ObjectArray;
		if(arrObj.Object == null) return;
		if((_object1 != arrObj.Object) || (_count1 != arrObj.Count) || (_alloworiginal1 != arrObj.AllowOriginal)
		   || (_useoffset1 != arrObj.UseOffset) || (_userot1 != arrObj.UseRotation) || (_usescale1 != arrObj.UseScale)
		   || (_posobject1 != arrObj.Object.transform.position) || (_rotobject1 != arrObj.Object.transform.rotation)){
			ObjectArray();
			_object1 = arrObj.Object; _count1 = arrObj.Count; _alloworiginal1 = arrObj.AllowOriginal;
			_useoffset1 = arrObj.UseOffset; _userot1 = arrObj.UseRotation; _usescale1 = arrObj.UseScale;
			_posobject1 = arrObj.Object.transform.position; _rotobject1 = arrObj.Object.transform.rotation;
		}
	}
	public void ObjectArray(){
		ObjectArray arrObj = target as ObjectArray;
		//Запоминание родителя
		if(arrObj.Object == null){ Debug.LogWarning("Object = null..."); return;}
		Transform parent = arrObj.Object.transform.parent;
		
		if(arrObj.Count > 0){
			for(int i = 0; i < arrObj.Count; i++){
				if(i == 0){
					//Если учитывается оригинал
					if(arrObj.AllowOriginal)
						i++;
					array = GameObject.Find("Array Object " + arrObj.gameObject.name);
					if(array != null)
						DestroyImmediate(array.gameObject);
					array = new GameObject("Array Object " + arrObj.gameObject.name);
					go = arrObj.gameObject;
				}
				GameObject newGO = Instantiate(arrObj.gameObject) as GameObject;
				newGO.name = (arrObj.gameObject.name + " " + i);
				DestroyImmediate(newGO.GetComponent<ObjectArray>());
				
				//Запоминаем масштаб
				Vector3 vectSc = arrObj.Object.transform.localScale;
				Vector3 vectGO = go.transform.localScale;
				
				//Если используется смещение
				if(arrObj.UseOffset){
					
					//Присваиваем родителя
					arrObj.Object.transform.parent = arrObj.gameObject.transform;
					newGO.transform.parent = go.transform;
					
					//Присваиваем позицию
					newGO.transform.localPosition = arrObj.Object.transform.localPosition;
				}
				//Если используется вращение
				if(arrObj.UseRotation){
					//Присваиваем вращение
					newGO.transform.localRotation = arrObj.Object.transform.localRotation;
				}
				//Если используется масштабирование
				if(arrObj.UseScale)
					vectGO = arrObj.Object.transform.localScale * i + arrObj.gameObject.transform.localScale;
				//Восстанавливаем родителя
				arrObj.Object.transform.parent = parent;
				//Восстанавливаем масштаб
				arrObj.Object.transform.localScale = vectSc;
				//Устанавливаем созданный объект точкой отсчёта
				go = newGO;
				//Созданный объект в массиве
				go.transform.parent = array.transform;
				//Восстанавливаем масштаб
				go.transform.localScale = vectGO;
			}
			
			if(arrObj.AllowOriginal)
				array.transform.parent = arrObj.gameObject.transform;
		}
	}
}
