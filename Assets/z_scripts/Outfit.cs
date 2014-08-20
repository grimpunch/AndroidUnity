using UnityEngine;
using System.Collections;

public class Outfit : MonoBehaviour {
	
	public GameObject[] Hats;
	public GameObject[] Accessories;
	public int hatnum;
	public int accnum;
	public int totalHats;
	public int totalAccs;
	public bool refreshNeeded = true;
	
	// Use this for initialization
	void Start () {
		totalAccs = Accessories.Length-1;
		totalHats = Hats.Length-1;
		hatnum = 1;
		accnum = 4;
	
	}
	
	void NextHat()
	{
		hatnum++;
		refreshNeeded = true;
	}
	void PrevHat()
	{
		hatnum--;
		refreshNeeded = true;
	}
	void NextAcc()
	{
		accnum++;
		refreshNeeded = true;
	}
	void PrevAcc()
	{
		accnum--;
		refreshNeeded = true;
	}
	
	void DeactivateOutfit()
	{
		for(int i = 0;i<totalAccs;i++)
		{
			Accessories[i].active = false;
		}
		for(int i = 0;i<totalHats;i++)
		{
			Hats[i].active = false;
		}
	}
	
	void HatSet()
	{
		Hats[hatnum].active = true;
	}
	
	void AccSet()
	{
		Accessories[accnum].active = true;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hatnum > totalHats){hatnum = 0;}
		if(hatnum < 0){hatnum = totalHats;}
		
		if(accnum > totalAccs){accnum = 0;}
		if(accnum < 0){accnum = totalAccs;}
		
		if(refreshNeeded == true){DeactivateOutfit();HatSet();AccSet();refreshNeeded = false;};
		
		
	}
}
