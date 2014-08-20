using UnityEngine;
using System.Collections;

public class HammerCollideScript : MonoBehaviour {

	public GameObject parentHammer;
	public float speed;
	public float volume;
	public float cooldown;
	public float pitch;
	// Use this for initialization
	void Start () {
	pitch = this.gameObject.audio.pitch;
	}
	
	void OnCollisionEnter(Collision collide)
	{
		if(parentHammer.audio.enabled==true)
		{
		parentHammer.audio.Stop();
		}
		if(collide.gameObject.tag == "Ball" && cooldown <0)
		{
			//this.audio.volume = volume;
				this.gameObject.audio.volume = volume;
				this.gameObject.audio.pitch = pitch + Random.Range(-0.15f,0.15f);
				this.gameObject.audio.Play();
			
			//Debug.Log("sound played");
			cooldown = 2.5f;
		}
		//parentHammer.rigidbody.velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	speed = parentHammer.hingeJoint.velocity;
		if (speed < 0)
		{
			speed = 0 - (speed);
		}
		
	//Debug.Log(speed);
	
	volume = (speed*0.005f);
	Mathf.Clamp(volume,0.5f,0.85f);
	//Debug.Log(volume);
		if(cooldown >=0)
		{cooldown--;}
	}
}
