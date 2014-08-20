using UnityEngine;
using System.Collections;

public class killball : MonoBehaviour {

	
	
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Ball")
		{
			other.gameObject.SendMessage("DestroyBall");
		}
	
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
