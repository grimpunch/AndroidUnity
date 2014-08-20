using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	public GameObject Impact;
	public float moveTimer;
	public float moveTimerSet;
	public float prevposx;
	public float prevposy;
	public GameObject[] eyes;
	public GameObject[] emotionObjects;
	
	
	
	// Use this for initialization
	void Start () {
	moveTimer = moveTimerSet;
	//BroadcastMessage("SetTargetBall",this.gameObject,SendMessageOptions.RequireReceiver);
	eyes = GameObject.FindGameObjectsWithTag("Pupil");
	emotionObjects = GameObject.FindGameObjectsWithTag("Face");	
		for(int j = 0;j<eyes.Length;j++)
		{
			eyes[j].SendMessage("SetTargetBall",this.gameObject,SendMessageOptions.RequireReceiver);
		}
		for(int j = 0;j<emotionObjects.Length;j++)
		{
			emotionObjects[j].SendMessage("SetTargetBall",this.gameObject,SendMessageOptions.RequireReceiver);
		}
		
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(Impact, pos, rot);
		//iTween.PunchScale(this.gameObject,new Vector3(0.8f,0.8f,0.8f),0.5f);
	}
	
	void DestroyBall()
	{
		
		if(PlayerPrefs.GetInt("BallsInPlay") > 0)
		{
		GameObject[] delballs = GameObject.FindGameObjectsWithTag("Ball");
		for(int i = 0; i < delballs.Length; i++)
		{
		Destroy(delballs[i].gameObject);
		}
			PlayerPrefs.SetInt("BallsInPlay", 0);
		}
		Destroy(gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	if (transform.position.x > 50 || transform.position.x < -50)
		{DestroyBall();}
	if (transform.position.y > 30 || transform.position.y < -30)
		{DestroyBall();}
	if(transform.position.x < -12.5 || transform.position.x > 12.5)
	{
	DestroyBall();
	}
	if(transform.position.y < -10 || transform.position.y > 10)
	{
	DestroyBall();
	}
		
	if((rigidbody.velocity.x > prevposx -0.02f || rigidbody.velocity.x < prevposx +0.02f)&&(rigidbody.velocity.y > prevposy -0.02f||rigidbody.velocity.y < prevposy +0.020f))
		{
			moveTimer -= Time.deltaTime;
			if(moveTimer < 0)
			{
			//transform.position = new Vector3(0,1,0);
			//	rigidbody.velocity.y= rigidbody.velocity.y+ 20;
			//DestroyBall();
			moveTimer = moveTimerSet;
			}
			
		}
		else{moveTimer = moveTimerSet;}
		prevposx = rigidbody.velocity.x;
		prevposy = rigidbody.velocity.y;
	}
}
