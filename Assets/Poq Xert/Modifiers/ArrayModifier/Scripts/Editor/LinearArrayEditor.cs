//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LinearArray))]
public class LinearArrayEditor : Editor {
	
	private GameObject array;
	
	private string[] RelaObjName = new string[]{"Collider", "Mesh"};
	private string[] RelaRotObjName = new string[]{"World", "Parent"};
	
	private Vector3 sizeVect = Vector3.zero;
	
	private Vector3 ConstRot1;
	private Vector3 RelaRot1;
	private Quaternion sizeRotVect;
	
	private Vector3 ConstScale1;
	private Vector3 RelaScale1;
	private Vector3 sizeScaleVect;
	
	#region Переменные изменений
	private int _count1 = 0;
	private bool _uselocalaxis = false;
	private int _dimensions1 = 0;
	private int _dimcount1 = 0;
	private int _dimcount2 = 0;
	private Vector3 _dimvect1 = Vector3.zero;
	private Vector3 _dimvect2 = Vector3.zero;
	private bool _constoffset1 = true;
	private bool _constrot1 = true;
	private bool _constscale1 = true;
	private Vector3 _constposvect1 = Vector3.zero;
	private Vector3 _constrotvect1 = Vector3.zero;
	private Vector3 _constscalevect1 = Vector3.zero;
	private bool _relaoffset1 = false;
	private bool _relarot1 = false;
	private bool _relascale1 = false;
	private Vector3 _relaposvect1 = Vector3.zero;
	private Vector3 _relarotvect1 = Vector3.zero;
	private Vector3 _relascalevect1 = Vector3.zero;
	private int _relaposobj1 = 0;
	private int _relarotobj1 = 0;
	private int _relascaleobj1 = 0;
	#endregion
	public override void OnInspectorGUI()
	{
		//Ссылка на компонент
		LinearArray arrLine = target as LinearArray;
		if(arrLine.renderer == null && arrLine.collider == null)
			arrLine.RelaOffset = false;
		//Отрисовка инспектора по умолчанию
		DrawDefaultInspector();
		
		ConstRot1 = arrLine.ConstRotVect;
		ConstScale1 = arrLine.ConstScaleVect;
		RelaRot1 = arrLine.RelaRotVect;
		RelaScale1 = arrLine.RelaScaleVect;
		
		EditorGUIUtility.LookLikeControls();
		arrLine.dimG = EditorGUILayout.Foldout(arrLine.dimG, "Dimensions Set");
		if(arrLine.dimG){
			arrLine.dimensions = EditorGUILayout.Popup("Dimensions", arrLine.dimensions, new string[]{"1D", "2D", "3D"});
			if(arrLine.dimensions > 0){
				arrLine.dimCount1 = EditorGUILayout.IntField("Count", arrLine.dimCount1);
				arrLine.dimVect1 = EditorGUILayout.Vector3Field("Offset", arrLine.dimVect1);
				if(arrLine.dimensions > 1){
					arrLine.dimCount2 = EditorGUILayout.IntField("Count", arrLine.dimCount2);
					arrLine.dimVect2 = EditorGUILayout.Vector3Field("Offset", arrLine.dimVect2);
				}
			}
		}
		arrLine.ConstG = EditorGUILayout.Foldout(arrLine.ConstG, "Constant Set");
		if(arrLine.ConstG){
			//Смещение
			arrLine.ConstOffset = EditorGUILayout.BeginToggleGroup("Constant Offset", arrLine.ConstOffset);
			arrLine.ConstVect = EditorGUILayout.Vector3Field("Offset", arrLine.ConstVect);
			EditorGUILayout.EndToggleGroup();
			//Вращение
			arrLine.ConstRot = EditorGUILayout.BeginToggleGroup("Constant Rotation", arrLine.ConstRot);
			ConstRot1 = EditorGUILayout.Vector3Field("Rotation", ConstRot1);
			EditorGUILayout.EndToggleGroup();
			arrLine.ConstRotVect = ConstRot1;
			
			//Масштабирование
			arrLine.ConstScale = EditorGUILayout.BeginToggleGroup("Constant Scale", arrLine.ConstScale);
			ConstScale1 = EditorGUILayout.Vector3Field("Scale", ConstScale1);
			EditorGUILayout.EndToggleGroup();
			arrLine.ConstScaleVect = ConstScale1;
		}
		
		//Относительные
		arrLine.RelaG = EditorGUILayout.Foldout(arrLine.RelaG, "Relative Set");
		if(arrLine.RelaG){
			//Смещение
			arrLine.RelaOffset = EditorGUILayout.BeginToggleGroup("Relative Offset", arrLine.RelaOffset);
			arrLine.RelaObj = EditorGUILayout.Popup("Relative Object: ", arrLine.RelaObj, RelaObjName);
			arrLine.RelaVect = EditorGUILayout.Vector3Field("Offset", arrLine.RelaVect);
			EditorGUILayout.EndToggleGroup();
			
			//Вращение
			arrLine.RelaRot = EditorGUILayout.BeginToggleGroup("Relative Rotation", arrLine.RelaRot);
			arrLine.RelaRotObj = EditorGUILayout.Popup("Relative Object", arrLine.RelaRotObj, RelaRotObjName);
			RelaRot1 = EditorGUILayout.Vector3Field("Rotation", RelaRot1);
			EditorGUILayout.EndToggleGroup();
			arrLine.RelaRotVect = RelaRot1;
			
			//Масштабирование
			arrLine.RelaScale = EditorGUILayout.BeginToggleGroup("Relative Scale", arrLine.RelaScale);
			arrLine.RelaScaleObj = EditorGUILayout.Popup("Relative Object", arrLine.RelaScaleObj, RelaRotObjName);
			RelaScale1 = EditorGUILayout.Vector3Field("Scale", RelaScale1);
			EditorGUILayout.EndToggleGroup();
			arrLine.RelaScaleVect = RelaScale1;
		}
		if(arrLine.collider == null && arrLine.RelaOffset)
			arrLine.RelaObj = 1;
		else if(arrLine.renderer == null && arrLine.RelaOffset)
			arrLine.RelaObj = 0;
		if(arrLine.transform.parent == null && arrLine.RelaRot)
			arrLine.RelaRotObj = 0;
		
		if(arrLine.RelaOffset || arrLine.ConstOffset){
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Separator();
			if(GUILayout.Button("Create Array"))
				LinearArray();
			EditorGUILayout.Separator();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Separator();
			if(GUILayout.Button("Apply"))
				DestroyImmediate(arrLine.GetComponent<LinearArray>());
			EditorGUILayout.Separator();
			if(GUILayout.Button("Cancel")){
				array = GameObject.Find("Array Line " + arrLine.gameObject.name);
				if(array != null)
					DestroyImmediate(array);
				DestroyImmediate(arrLine.GetComponent<LinearArray>());
			}
			EditorGUILayout.Separator();
			EditorGUILayout.EndHorizontal();
		}
	}
	void OnSceneGUI(){
		LinearArray arrLine = (LinearArray)target as LinearArray;
		if((_count1 != arrLine.Count) || (_uselocalaxis != arrLine.UseLocalAxis) || (_dimensions1 != arrLine.dimensions)
		   || (_dimcount1 != arrLine.dimCount1) || (_dimcount2 != arrLine.dimCount2) || (_dimvect1 != arrLine.dimVect1)
		   || (_dimvect2 != arrLine.dimVect2) || (_constoffset1 != arrLine.ConstOffset) || (_constrot1 != arrLine.ConstRot)
		   || (_constscale1 != arrLine.ConstScale) || (_constposvect1 != arrLine.ConstVect) || (_constrotvect1 != arrLine.ConstRotVect)
		   || (_constscalevect1 != arrLine.ConstScaleVect) || (_relaoffset1 != arrLine.RelaOffset) || (_relarot1 != arrLine.RelaRot)
		   || (_relascale1 != arrLine.RelaScale) || (_relaposvect1 != arrLine.RelaVect) || (_relarotvect1 != arrLine.RelaRotVect)
		   || (_relascalevect1 != arrLine.RelaScaleVect) || (_relaposobj1 != arrLine.RelaObj) || (_relarotobj1 != arrLine.RelaRotObj)
		   || (_relascaleobj1 != arrLine.RelaScaleObj)){
			LinearArray();
			_count1 = arrLine.Count; _uselocalaxis = arrLine.UseLocalAxis; _dimensions1 = arrLine.dimensions;
			_dimcount1 = arrLine.dimCount1; _dimcount2 = arrLine.dimCount2; _dimvect1 = arrLine.dimVect1;
		    _dimvect2 = arrLine.dimVect2; _constoffset1 = arrLine.ConstOffset; _constrot1 = arrLine.ConstRot;
		    _constscale1 = arrLine.ConstScale; _constposvect1 = arrLine.ConstVect; _constrotvect1 = arrLine.ConstRotVect;
		    _constscalevect1 = arrLine.ConstScaleVect; _relaoffset1 = arrLine.RelaOffset; _relarot1 = arrLine.RelaRot;
		    _relascale1 = arrLine.RelaScale; _relaposvect1 = arrLine.RelaVect; _relarotvect1 = arrLine.RelaRotVect;
		    _relascalevect1 = arrLine.RelaScaleVect; _relaposobj1 = arrLine.RelaObj; _relarotobj1 = arrLine.RelaRotObj;
		    _relascaleobj1 = arrLine.RelaScaleObj;
		}
	}
	public void LinearArray(){
				
		//Ссылка на компонент
		LinearArray arrLine = target as LinearArray;
		
		//if(arrLine.RelaOffset)
		//	arrLine.UseLocalAxis = false;
		if(arrLine.Count > 0){
			for(int i = 1; i < arrLine.Count; i++){
				if(i == 1){
					array = GameObject.Find("Array Line " + arrLine.gameObject.name);
					if(array != null)
						DestroyImmediate(array.gameObject);
					array = new GameObject("Array Line " + arrLine.gameObject.name);
					array.transform.position = arrLine.transform.position;
				}
				GameObject go = GameObject.Find(arrLine.gameObject.name + " " + i);
				if(go != null)
					DestroyImmediate(go);
				go = Instantiate(arrLine.gameObject) as GameObject;
				go.name = (arrLine.gameObject.name + " " + i);
				DestroyImmediate(go.GetComponent<LinearArray>());
				#region Смещение
				if(arrLine.RelaOffset){
					if(arrLine.RelaObj == 0)
						sizeVect = arrLine.gameObject.collider.bounds.size;
					if(arrLine.RelaObj == 1)
						sizeVect = arrLine.gameObject.renderer.bounds.size;
				}
				
				Vector3 Vect1 = arrLine.ConstVect;
				Vector3 RelaVect1 = new Vector3(arrLine.RelaVect.x * sizeVect.x,
							                     			arrLine.RelaVect.y * sizeVect.y, arrLine.RelaVect.z * sizeVect.z);
				if(arrLine.UseLocalAxis){
					Vect1 = arrLine.transform.TransformDirection(Vect1);
					RelaVect1 = arrLine.transform.TransformDirection(RelaVect1);
					sizeVect = arrLine.transform.TransformDirection(sizeVect) - arrLine.transform.localScale;
				}
				Vector3 vect = arrLine.transform.position + (Vect1 * i);
				if(arrLine.ConstOffset){
					go.transform.position = vect;
					if(arrLine.RelaOffset){
						arrLine.gameObject.transform.position += RelaVect1 * i;
					}//-if
				}//-if
				else if(arrLine.RelaOffset){
					go.transform.position += RelaVect1 * i;
				}//-if
				#endregion
				#region Вращение
				if(arrLine.RelaRot){
					if(arrLine.RelaRotObj == 0)
						sizeRotVect = arrLine.transform.rotation;
					if(arrLine.RelaObj == 1)
						sizeRotVect = arrLine.transform.localRotation;
				}
				Quaternion RelaRotVect1 = Quaternion.Euler((RelaRot1.x + sizeRotVect.x) * i,
							                     			(arrLine.RelaRotVect.y + sizeRotVect.y) * i, (arrLine.RelaRotVect.z + sizeRotVect.z) * i);
				Debug.Log(RelaRotVect1.ToString());
				if(arrLine.ConstRot){
					go.transform.rotation = arrLine.transform.rotation * Quaternion.Euler(ConstRot1 * i);
					if(arrLine.RelaRot){
						go.transform.rotation *= RelaRotVect1;
					}//-if
				}//-if
				else if(arrLine.RelaRot){
					go.transform.rotation = arrLine.transform.rotation;
					go.transform.rotation *= RelaRotVect1;
				}//-if
				#endregion
				#region Масштабирование
				if(arrLine.RelaScale){
					if(arrLine.RelaScaleObj == 0)
						sizeScaleVect = arrLine.transform.lossyScale;
					if(arrLine.RelaObj == 1)
						sizeScaleVect = arrLine.transform.localScale;
				}
				if(arrLine.ConstScale){
					go.transform.localScale = arrLine.transform.lossyScale + ConstScale1 * i;
					if(arrLine.RelaScale){
						go.transform.localScale += sizeScaleVect + RelaScale1 * i;
					}//-if
				}//-if
				else if(arrLine.RelaScale){
						go.transform.localScale += sizeScaleVect + RelaScale1 * i;
				}//-if
				#endregion
				go.transform.parent = array.transform;
			}//-for
			if(arrLine.dimensions > 0){
				GameObject[] goD1 = new GameObject[arrLine.dimCount1 - 1];
				GameObject[] origD1 = new GameObject[arrLine.dimCount1 - 1];
				for(int i = 1; i < arrLine.dimCount1; i++){
					origD1[i-1] = Instantiate(arrLine.gameObject, arrLine.transform.position + arrLine.dimVect1 * i, arrLine.transform.rotation) as GameObject;
					DestroyImmediate(origD1[i-1].GetComponent<LinearArray>());
					origD1[i-1].name = (arrLine.gameObject.name + " " + (arrLine.Count + i - 1));
					goD1[i-1] = Instantiate(array, array.transform.position + arrLine.dimVect1 * i, array.transform.rotation) as GameObject;
					goD1[i-1].name = array.name + " " + i;
				}
				for(int j = 0; j < goD1.Length; j++){
					while(goD1[j].transform.childCount > 0){
						Transform go = goD1[j].transform.GetChild(0);
						go.gameObject.name = arrLine.name + " " + (array.transform.childCount + 1).ToString();
						go.parent = array.transform;
					}
					origD1[j].name = arrLine.name + " " + (array.transform.childCount + 1).ToString();
					origD1[j].transform.parent = array.transform;
					DestroyImmediate(goD1[j]);
				}
				if(arrLine.dimensions > 1){
					GameObject[] goD2 = new GameObject[arrLine.dimCount2 - 1];
					GameObject[] origD2 = new GameObject[arrLine.dimCount2 - 1];
					for(int i = 1; i < arrLine.dimCount2; i++){
						origD2[i-1] = Instantiate(arrLine.gameObject, arrLine.transform.position + arrLine.dimVect2 * i, arrLine.transform.rotation) as GameObject;
						DestroyImmediate(origD2[i-1].GetComponent<LinearArray>());
						origD2[i-1].name = (arrLine.gameObject.name + " " + (arrLine.Count + i - 1));
						goD2[i-1] = Instantiate(array, array.transform.position + arrLine.dimVect2 * i, array.transform.rotation) as GameObject;
						goD2[i-1].name = array.name + " " + i;
					}
					for(int j = 0; j < goD2.Length; j++){
						while(goD2[j].transform.childCount > 0){
							Transform go = goD2[j].transform.GetChild(0);
							go.gameObject.name = arrLine.name + " " + (array.transform.childCount + 1).ToString();
							go.parent = array.transform;
						}
						origD2[j].name = arrLine.name + " " + (array.transform.childCount + 1).ToString();
						origD2[j].transform.parent = array.transform;
						DestroyImmediate(goD2[j]);
					}
				}
			}
		}//-if
		
	}//-Function
	
}//-Class
