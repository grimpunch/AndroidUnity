using UnityEngine;
using System.Collections;

public class SplashScreenScript : MonoBehaviour {
	
	public bool played = false;
	public bool animationended = false;
	// Use this for initialization
	void Start () {
		
	}
	
	
	void playanimation()
	{
		animation.Play();
	}
	
	void animationFunction()
	{
		animationended = true;
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if(Application.isLoadingLevel == false && played==false)
		{playanimation();played = true;}
		if(played == true && animationended == true)
		{
			Application.LoadLevel(1);
		}
	}
}
