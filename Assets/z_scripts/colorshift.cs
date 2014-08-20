using UnityEngine;
using System.Collections;

public class colorshift : MonoBehaviour {
	
	
	// You can then set the colours in the inspector
public Color[] colors = new Color[0];
// Current shown colour
public int currentIndex = 0;
// Seconds between change of colour
public float changeColourTime = 1;
// Last time we changed a colour
private float lastChange = 0.0f;

void FixedUpdate()
{
    if (colors.Length > 0 && lastChange + changeColourTime < Time.time)
    {
        lastChange = Time.time;
        currentIndex = (currentIndex + 1) % colors.Length;

        renderer.material.color = colors[currentIndex];
    }
}
	// Use this for initialization
	void Start () {
	PlayerPrefs.SetFloat("changetime", 1);
	}
	
	// Update is called once per frame
	void Update () {
	changeColourTime = PlayerPrefs.GetFloat("changetime");
	changeColourTime = changeColourTime*Random.Range(0.85f,1.15f);
	}
}
