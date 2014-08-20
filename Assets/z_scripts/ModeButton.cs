using UnityEngine;
using System.Collections;

public class ModeButton : MonoBehaviour {
	
	public GameObject Logic;
	public GameObject OutfitHandler;
	public Vector3 amount;
	public int ClickAmount = 0;
	bool clicked;
	public enum ButtonType{SP,VS,CPU,Start,Music,AI,Quit,Rematch,Menu,HatNext,HatPrev,AccNext,AccPrev}
	public ButtonType bType;
	
	// Use this for initialization
	void Start () {
	Logic = GameObject.FindGameObjectWithTag("Menu");
		amount = new Vector3(0.2f,0.1f,0);
		if(clicked == false)
		{
		iTween.ShakePosition(this.gameObject,new Hashtable(){{"oncomplete","resetClicked"},{"ignoretimescale",true},{"amount",amount},{"time",0.4f}});
		iTween.ShakeRotation(this.gameObject,new Vector3(20,20,0),0.4f);
		clicked = true;
		}
		
	}
	
	void Selected()
	{
		switch(bType)
		{
			case ButtonType.SP:
			{
			Logic.SendMessage("SetGameMode",1,SendMessageOptions.RequireReceiver);
			break;
			}
			case ButtonType.VS:
			{
			Logic.SendMessage("SetGameMode",2,SendMessageOptions.RequireReceiver);
			break;
			}
			case ButtonType.CPU:
			{
			Logic.SendMessage("SetGameMode",0,SendMessageOptions.RequireReceiver);
			break;	
			}
			case ButtonType.Start:
			{
			if(ClickAmount == 0)
			{
			Logic.SendMessage("Customise",SendMessageOptions.RequireReceiver);
			ClickAmount = 1;
			}
			else if(ClickAmount == 1)
			{
			Logic.SendMessage("StartGame",SendMessageOptions.RequireReceiver);
			ClickAmount = 0;
			}
			break;	
			}
			case ButtonType.Menu:
			{
			Logic.SendMessage("Menu",SendMessageOptions.RequireReceiver);
			break;	
			}
			case ButtonType.Rematch:
			{
			Logic.SendMessage("NewGame",SendMessageOptions.RequireReceiver);
			break;	
			}
			case ButtonType.Music:
			{
			Logic.SendMessage("MusicToggle",SendMessageOptions.RequireReceiver);
			break;
			}
			case ButtonType.AI:
			{
			Logic.SendMessage("AIToggle",SendMessageOptions.RequireReceiver);
			break;
			}
			case ButtonType.Quit:
			{
			Application.Quit();
			break;
			}
			case ButtonType.HatPrev:
			{
			OutfitHandler.SendMessage("PrevHat",SendMessageOptions.RequireReceiver);
			break;
			}
			case ButtonType.HatNext:
			{
			OutfitHandler.SendMessage("NextHat",SendMessageOptions.RequireReceiver);
			break;
			}
			case ButtonType.AccPrev:
			{
			OutfitHandler.SendMessage("PrevAcc",SendMessageOptions.RequireReceiver);
			break;
			}
			case ButtonType.AccNext:
			{
			OutfitHandler.SendMessage("NextAcc",SendMessageOptions.RequireReceiver);
			break;
			}
			
			
			
		}
		if(clicked == false)
		{
		iTween.ShakePosition(this.gameObject,new Hashtable(){{"oncomplete","resetClicked"},{"ignoretimescale",true},{"amount",amount},{"time",0.4f}});
		iTween.ShakeRotation(this.gameObject,new Vector3(20,20,0),0.4f);
			clicked = true;
		}
		 Debug.Log("Selected",this); 
		
	}
	void resetClicked()
		{
			clicked = false;	
		}
	
	// Update is called once per frame
	void Update () {
	
	}
}
