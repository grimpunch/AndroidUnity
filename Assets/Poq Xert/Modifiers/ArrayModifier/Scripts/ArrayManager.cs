//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using System.Collections;

public class ArrayManager : MonoBehaviour {
	
	public void NewArray(int type){
		switch(type){
			case 0:
				gameObject.AddComponent(typeof(LinearArray));
				break;
			case 1:
				gameObject.AddComponent(typeof(CurveArray));
				break;
			case 2:
				gameObject.AddComponent(typeof(ObjectArray));
				break;
		}
		DestroyImmediate(gameObject.GetComponent<ArrayManager>());
	}
}
