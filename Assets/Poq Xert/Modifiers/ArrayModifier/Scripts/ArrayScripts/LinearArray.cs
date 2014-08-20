//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using System.Collections;

public class LinearArray : MonoBehaviour {
	
	public int Count = 2;
	public bool UseLocalAxis = true;
	//Смещение
	private bool ConstantOffset = true;
	private Vector3 ConstantVect = Vector3.one;
	
	private bool RelativeOffset = false;
	private Vector3 RelativeVect = Vector3.zero;
	//Вращение
	private bool ConstantRot = true;
	private Vector3 ConstantRotVect = Vector3.zero;
	
	private bool RelativeRot = false;
	private Vector3 RelativeRotVect = Vector3.zero;
	//Масштаб
	private bool ConstantScale = true;
	private Vector3 ConstantScaleVect = Vector3.zero;
	
	private bool RelativeScale = false;
	private Vector3 RelativeScaleVect = Vector3.zero;
	//Отображение настроек
	private bool ConstGroup = false;
	private bool RelaGroup = false;
	
	//Относительные объекты
	private int RelativeObject = 0;
	private int RelativeRotObject = 0;
	private int RelativeScaleObject = 0;
	
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
	
	#region Смещение
	public bool ConstOffset{
		set{ConstantOffset = value;}
		get{return ConstantOffset;}
	}
	public bool RelaOffset{
		set{RelativeOffset = value;}
		get{return RelativeOffset;}
	}
	public Vector3 ConstVect{
		set{ConstantVect = value;}
		get{return ConstantVect;}
	}
	public Vector3 RelaVect{
		set{RelativeVect = value;}
		get{return RelativeVect;}
	}
	#endregion
	#region Вращение
	public bool ConstRot{
		set{ConstantRot = value;}
		get{return ConstantRot;}
	}
	public Vector3 ConstRotVect{
		set{ConstantRotVect = value;}
		get{return ConstantRotVect;}
	}
	public bool RelaRot{
		set{RelativeRot = value;}
		get{return RelativeRot;}
	}
	public Vector3 RelaRotVect{
		set{RelativeRotVect = value;}
		get{return RelativeRotVect;}
	}
	#endregion
	#region Масштабирование
	public bool ConstScale{
		set{ConstantScale = value;}
		get{return ConstantScale;}
	}
	public Vector3 ConstScaleVect{
		set{ConstantScaleVect = value;}
		get{return ConstantScaleVect;}
	}
	public bool RelaScale{
		set{RelativeScale = value;}
		get{return RelativeScale;}
	}
	public Vector3 RelaScaleVect{
		set{RelativeScaleVect = value;}
		get{return RelativeScaleVect;}
	}
	#endregion
	#region Относительные объекты
	public int RelaObj{
		set{RelativeObject = value;}
		get{return RelativeObject;}
	}
	public int RelaRotObj{
		set{RelativeRotObject = value;}
		get{return RelativeRotObject;}
	}
	public int RelaScaleObj{
		set{RelativeScaleObject = value;}
		get{return RelativeScaleObject;}
	}
	#endregion
	#region Отображение настроек
	public bool ConstG{
		set{ConstGroup = value;}
		get{return ConstGroup;}
	}
	public bool RelaG{
		set{RelaGroup = value;}
		get{return RelaGroup;}
	}
	#endregion
}
