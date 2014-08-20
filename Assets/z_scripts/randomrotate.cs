using UnityEngine;
using System.Collections;

public class randomrotate : MonoBehaviour {
	
	
	public float timetobeat;
	public float bpm;
	public float scaleAmount;
	public float rotateAmount;
	
	//public GameObject[] Cubes;
	
	public bool MusicStarted = false;
	bool on = true;
	public float seconds = 60;
	
	
	// Use this for initialization
	void Start () {
	
		//Cubes = GameObject.FindGameObjectsWithTag("bgcube");
		
	if(PlayerPrefs.GetInt("MusicOn")==1)
		{
			StartCoroutine("Beat");
		}
	
		
		
		//THE GREAT BPM DEFINE OF ALL TIME
		bpm = 140;
		//////////////////////////////////
		//scaleAmount = 2.5f;
	}
	
	 IEnumerator Beat() {
		MusicStarted = true;
		while(on)
		{
			timetobeat = ((60/bpm));
			
		PlayerPrefs.SetFloat("changetime", 1f);
       yield return new WaitForSeconds(timetobeat);
			
			iTween.PunchScale(this.gameObject,new Vector3(scaleAmount,scaleAmount,scaleAmount),timetobeat/2f);
		if(this.gameObject.tag == "Speaker")
			{}
			else{
			transform.Rotate(new Vector3(0,0,rotateAmount) * Time.deltaTime*2,Space.Self); 
			}
        PlayerPrefs.SetFloat("changetime", 0.1f);
			
		}
    }
	
	// Update is called once per frame
	void Update () 
   	{ 
		if(PlayerPrefs.GetInt("MusicOn")==1 && MusicStarted == false)
		{
			StartCoroutine("Beat");
		}
		if(PlayerPrefs.GetInt("MusicOn")==0)
		{
			StopCoroutine("Beat");
			MusicStarted = false;
		}
		
		
	}
}
