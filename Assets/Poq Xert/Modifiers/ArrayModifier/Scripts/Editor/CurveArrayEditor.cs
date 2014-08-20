//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CurveArray))]
public class CurveArrayEditor : Editor {
	
	public bool CreateObj = false;
	private GameObject array;
	private float Length = 0;
	
	#region Переменные изменений
	private int _count1 = 0;
	private float _distance1 = 0;
	private bool _allowcount1 = false;
	private bool _alloworiginal1 = true;
	private int _dimensions1 = 0;
	private int _dimcount1 = 0;
	private int _dimcount2 = 0;
	private Vector3 _dimvect1 = Vector3.zero;
	private Vector3 _dimvect2 = Vector3.zero;
	#endregion
	//Отрисовка инспектора компонента
	public override void OnInspectorGUI()
	{
		//Отрисовка инспектора по умолчанию
		DrawDefaultInspector();
		
		//Ссылка на компонент
		CurveArray arrCurve = target as CurveArray;
		//Вычисляем длину кривой
		Length = 0;
		for(int i = 1; i < 50; i++){
			float t1 = (i - 1f)/49f;
			float t2 = i/49f;
			Length += (arrCurve.Calculate(t1) - arrCurve.Calculate(t2)).magnitude;
		}
		EditorGUIUtility.LookLikeControls();
		arrCurve.dimG = EditorGUILayout.Foldout(arrCurve.dimG, "Dimensions Set");
		if(arrCurve.dimG){
			arrCurve.dimensions = EditorGUILayout.Popup("Dimensions", arrCurve.dimensions, new string[]{"1D", "2D", "3D"});
			if(arrCurve.dimensions > 0){
				arrCurve.dimCount1 = EditorGUILayout.IntField("Count", arrCurve.dimCount1);
				arrCurve.dimVect1 = EditorGUILayout.Vector3Field("Offset", arrCurve.dimVect1);
				if(arrCurve.dimensions > 1){
					arrCurve.dimCount2 = EditorGUILayout.IntField("Count", arrCurve.dimCount2);
					arrCurve.dimVect2 = EditorGUILayout.Vector3Field("Offset", arrCurve.dimVect2);
				}
			}
		}
		if(arrCurve.Count == 0)
			arrCurve.Count = 1;
		if(arrCurve.dimCount1 == 0)
			arrCurve.dimCount1 = 1;
		if(arrCurve.dimCount2 == 0)
			arrCurve.dimCount2 = 1;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Separator();
		if(GUILayout.Button("Create Array"))
			CurveArray();
		EditorGUILayout.Separator();
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Separator();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Separator();
		if(GUILayout.Button("Apply")){
			DestroyImmediate(arrCurve.GetComponent<CurveArray>());
			DestroyImmediate(arrCurve.SetGetP0);
			DestroyImmediate(arrCurve.SetGetP1);
			DestroyImmediate(arrCurve.SetGetP2);
			DestroyImmediate(arrCurve.SetGetP3);
		}
		EditorGUILayout.Separator();
		if(GUILayout.Button("Cancel")){
			array = GameObject.Find("Array Curve " + arrCurve.gameObject.name);
			if(array != null)
				DestroyImmediate(array);
			DestroyImmediate(arrCurve.GetComponent<CurveArray>());
			DestroyImmediate(arrCurve.SetGetP0);
			DestroyImmediate(arrCurve.SetGetP1);
			DestroyImmediate(arrCurve.SetGetP2);
			DestroyImmediate(arrCurve.SetGetP3);
		}
		EditorGUILayout.Separator();
		EditorGUILayout.EndHorizontal();
		// отображаем длину кривой в инспекторе
	    GUILayout.Label(string.Format("Curve length: {0}", Length));
	}
	
	//Отрисовка в сцене
	public void OnSceneGUI(){
		CurveArray arrCurve = target as CurveArray;
		if(arrCurve.SetGetP0 == null || arrCurve.SetGetP1 == null || arrCurve.SetGetP2 == null || arrCurve.SetGetP3 == null)
			CreateObj = false;
		else
			CreateObj = true;
		if(arrCurve){
			if(CreateObj){
				//Рисуем манипуляторы
				Handles.DrawLine(arrCurve.SetGetP0.transform.position, arrCurve.SetGetP1.transform.position);
				Handles.DrawLine(arrCurve.SetGetP2.transform.position, arrCurve.SetGetP3.transform.position);
	
				//Манипуляторы для контрольных точек
				Quaternion rot = Quaternion.identity;
	            float size = HandleUtility.GetHandleSize(arrCurve.SetGetP0.transform.position) * 0.2f;
	            arrCurve.SetGetP0.transform.position = Handles.FreeMoveHandle(arrCurve.SetGetP0.transform.position, rot, size, Vector3.zero, Handles.SphereCap);
	            arrCurve.SetGetP1.transform.position = Handles.FreeMoveHandle(arrCurve.SetGetP1.transform.position, rot, size, Vector3.zero, Handles.SphereCap);
	            arrCurve.SetGetP2.transform.position = Handles.FreeMoveHandle(arrCurve.SetGetP2.transform.position, rot, size, Vector3.zero, Handles.SphereCap);
	            arrCurve.SetGetP3.transform.position = Handles.FreeMoveHandle(arrCurve.SetGetP3.transform.position, rot, size, Vector3.zero, Handles.SphereCap);
			}
		}
		if(GUI.changed)
			EditorUtility.SetDirty(target);
		if((_count1 != arrCurve.Count) || (_distance1 != arrCurve.Distance) || (_allowcount1 != arrCurve.AllowCount)
		   || (_alloworiginal1 != arrCurve.AllowOriginal) || (_dimensions1 != arrCurve.dimensions) || (_dimcount1 !=arrCurve.dimCount1)
		   || (_dimcount2 != arrCurve.dimCount2) || (_dimvect1 != arrCurve.dimVect1) || (_dimvect2 != arrCurve.dimVect2)){
			CurveArray();
			_count1 = arrCurve.Count; _distance1 = arrCurve.Distance; _allowcount1 = arrCurve.AllowCount;
		    _alloworiginal1 = arrCurve.AllowOriginal; _dimensions1 = arrCurve.dimensions; _dimcount1 = arrCurve.dimCount1;
		    _dimcount2 = arrCurve.dimCount2; _dimvect1 = arrCurve.dimVect1; _dimvect2 = arrCurve.dimVect2;
		}
	}
	public void CurveArray(){
		
		//Ссылка на компонент
		CurveArray arrCurve = target as CurveArray;
		if(arrCurve.Count > 0){
			for(int i = 0; i < arrCurve.Count; i++){
				if(i == 0){
					array = GameObject.Find("Array Curve " + arrCurve.gameObject.name);
					if(array != null)
						if(arrCurve.transform.parent == array.transform)
							arrCurve.transform.parent = null;
						DestroyImmediate(array.gameObject);
					array = new GameObject("Array Curve " + arrCurve.gameObject.name);
					if(arrCurve.AllowOriginal){
						i = 1;
						arrCurve.transform.position = arrCurve.SetGetP0.transform.position;
					}
				}
				GameObject go = GameObject.Find(arrCurve.gameObject.name + " " + i);
				if(go != null)
					DestroyImmediate(go);
				go = Instantiate(arrCurve.gameObject) as GameObject;
				go.name = (arrCurve.gameObject.name + " " + i);
				DestroyImmediate(go.GetComponent<CurveArray>());
				float pos = Length/arrCurve.Count/Length * i * arrCurve.Distance;
				if(!arrCurve.AllowCount)
					pos = Length/(Length/arrCurve.Distance)/Length * i;
				go.transform.position = arrCurve.Calculate(pos);
				go.transform.parent = array.transform;
			}
			if(arrCurve.dimensions > 0){
				GameObject[] goD1 = new GameObject[arrCurve.dimCount1 - 1];
				GameObject[] origD1 = new GameObject[arrCurve.dimCount1 - 1];
				for(int i = 1; i < arrCurve.dimCount1; i++){
					origD1[i-1] = Instantiate(arrCurve.gameObject, arrCurve.transform.position + arrCurve.dimVect1 * i, arrCurve.transform.rotation) as GameObject;
					DestroyImmediate(origD1[i-1].GetComponent<CurveArray>());
					origD1[i-1].name = (arrCurve.gameObject.name + " " + (arrCurve.Count + i - 1));
					goD1[i-1] = Instantiate(array, array.transform.position + arrCurve.dimVect1 * i, array.transform.rotation) as GameObject;
					goD1[i-1].name = array.name + " " + i;
				}
				for(int j = 0; j < goD1.Length; j++){
					while(goD1[j].transform.childCount > 0){
						Transform go = goD1[j].transform.GetChild(0);
						go.gameObject.name = arrCurve.name + " " + (array.transform.childCount + 1).ToString();
						go.parent = array.transform;
					}
					origD1[j].name = arrCurve.name + " " + (array.transform.childCount + 1).ToString();
					origD1[j].transform.parent = array.transform;
					DestroyImmediate(goD1[j]);
				}
				if(arrCurve.dimensions > 1){
					GameObject[] goD2 = new GameObject[arrCurve.dimCount2 - 1];
					GameObject[] origD2 = new GameObject[arrCurve.dimCount2 - 1];
					for(int i = 1; i < arrCurve.dimCount2; i++){
						origD2[i-1] = Instantiate(arrCurve.gameObject, arrCurve.transform.position + arrCurve.dimVect2 * i, arrCurve.transform.rotation) as GameObject;
						DestroyImmediate(origD2[i-1].GetComponent<CurveArray>());
						origD2[i-1].name = (arrCurve.gameObject.name + " " + (arrCurve.Count + i - 1));
						goD2[i-1] = Instantiate(array, array.transform.position + arrCurve.dimVect2 * i, array.transform.rotation) as GameObject;
						goD2[i-1].name = array.name + " " + i;
					}
					for(int j = 0; j < goD2.Length; j++){
						while(goD2[j].transform.childCount > 0){
							Transform go = goD2[j].transform.GetChild(0);
							go.gameObject.name = arrCurve.name + " " + (array.transform.childCount + 1).ToString();
							go.parent = array.transform;
						}
						origD2[j].name = arrCurve.name + " " + (array.transform.childCount + 1).ToString();
						origD2[j].transform.parent = array.transform;
						DestroyImmediate(goD2[j]);
					}
				}
			}
		}
	}
}
