using UnityEngine;
using System.Collections;

public class Wobble : MonoBehaviour {
	
	
	public Vector3 WobbleAmount					;
	public float WobbleTime					;
	public Vector3 WobbleAmountSet				;
	public float WobbleTimeSet					;
	public Vector3 OriginalPosition			;
	public Vector3 OriginalScale				;
	public Quaternion OriginalRotation			;

	public Color HitColor;
	public Color originalColor;
	
	
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Ball")
		{
			
			this.renderer.material.color = originalColor;
			
			
			WobbleAmount = WobbleAmountSet;
			WobbleTime = WobbleTimeSet;
			
			iTween.ColorFrom(this.gameObject,HitColor,WobbleTime/3);
			iTween.ColorTo(this.gameObject,originalColor,2);
			iTween.PunchPosition(this.gameObject,WobbleAmount,WobbleTime);
			this.audio.Play();
			
			//this.renderer.material.color = HitColor;
			//INSERT SOUND EFFECT			
			//wobble = true;
		}
	
	}
	
	
	// Use this for initialization
	void Start () 
	{
	originalColor = this.renderer.material.color;
		OriginalPosition = transform.position;
			OriginalScale = transform.localScale;
				OriginalRotation = transform.rotation;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/*if(wobble)
		{
			
			iTween.ColorFrom(this.gameObject,HitColor,WobbleTime/3);
			iTween.PunchPosition(this.gameObject,WobbleAmount,WobbleTime);
			wobble = false;
			//iTween.ColorFrom(this.gameObject,originalColor,WobbleTime);
		}
		if(wobble == false)
		{
			iTween.ColorTo(this.gameObject,originalColor,2);
		}
		*/
		
		
		
	
	}
}
