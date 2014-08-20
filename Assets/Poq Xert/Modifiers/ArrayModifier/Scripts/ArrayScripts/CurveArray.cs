//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using System.Collections;
public class CurveArray : MonoBehaviour {
	
	//опорные точки
	public Color CurveColor = Color.green;
	public int Count = 2;
	public float Distance = 1;
	public bool AllowCount = false;
	public bool AllowOriginal = true;
	private GameObject P0;
	private GameObject P1;
	private GameObject P2;
	private GameObject P3;
	
	//Измерения
	[HideInInspector]
	public bool dimG = false;
	[HideInInspector]
	public int dimensions = 0;
	[HideInInspector]
	public	Vector3 dimVect1 = Vector3.zero;
	[HideInInspector]
	public	Vector3 dimVect2 = Vector3.zero;
	[HideInInspector]
	public int dimCount1 = 1;
	[HideInInspector]
	public int dimCount2 = 1;
	
	public void CreateObject(){
		P0 = new GameObject("Curve Pos 0");
		P0.transform.position = gameObject.transform.position;
		P1 = new GameObject("Curve Pos 1");
		P1.transform.position = gameObject.transform.position + new Vector3(20, 0, 0);
		P2 = new GameObject("Curve Pos 2");
		P2.transform.position = gameObject.transform.position + new Vector3(40, 0, 0);
		P3 = new GameObject("Curve Pos 3");
		P3.transform.position = gameObject.transform.position + new Vector3(60, 0, 0);
		//Debug.Log(gameObject.transform.renderer.bounds.extents.ToString());
	}
	// интерполяция по кривой используя формулу кривой Bezier третьего порядка
    public Vector3 Calculate(float t)
    {
		if(P0 == null || P1 == null || P2 == null || P3 == null){
			CreateObject();
		}
	    float t1 = 1 - t;
	    return t1 * t1 * t1 * P0.transform.position + 3 * t * t1 * t1 * P1.transform.position +
				3 * t * t * t1 * P2.transform.position + t * t * t * P3.transform.position;
    }
	//"Рисование" кривой
	public void OnDrawGizmos(){
		Gizmos.color = CurveColor;
		//Отрисовка кривой как несколько сегментов
		for(int i = 1; i < 50; i++){
			float t1 = (i - 1f)/49f;
			float t2 = i/49f;
			Gizmos.DrawLine(Calculate(t1), Calculate(t2));
		}
	}
	public GameObject SetGetP0{
		set{P0 = value;}
		get{return P0;}
	}
	public GameObject SetGetP1{
		set{P1 = value;}
		get{return P1;}
	}
	public GameObject SetGetP2{
		set{P2 = value;}
		get{return P2;}
	}
	public GameObject SetGetP3{
		set{P3 = value;}
		get{return P3;}
	}

}
