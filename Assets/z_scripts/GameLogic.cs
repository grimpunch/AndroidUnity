using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {
	
	//GameType : 1v1, 1vAI etc
	public enum GameType{Head2Head, HumanVsCPU , CPUVSCPU};
	public GameType gametype;
	//GameState : Pre-Game settings , Game Play , Post Game wrap up.
	public enum GameState {PreGame, GamePlay, PostGame};
	public static GameState gameState;
	//PlayerTurnState : Whose turn is it? Red or Blue.
	
	//MenuGUIControl
	public GUISkin GUISKIN;
	public GUISkin GUISKIN2;
	public Font font;
	public Color color;
	
	/* OLD GUI
	private Rect PreGameWindow = new Rect (0,0,Screen.width,Screen.height);
	private Rect PostGameWindow = new Rect (Screen.width*0.2f,Screen.height*0.1f,Screen.width*0.6f,Screen.height*0.8f);
	private Rect PauseWindow =new Rect (Screen.width * 0.30f,Screen.height*0.2f,Screen.width * 0.40f,Screen.height*0.60f);
	*/
	
	
	//Players
	public GameObject RedPaddle;
	public GameObject BluePaddle;
	public GameObject RedSpawn;
	public GameObject BlueSpawn;
	public GameObject[] Joysticks;
	
	
	//GUI/// 
	//MENU//
	public GameObject StartGameButton;
	public GameObject SPButton;
	public GameObject VSButton;
	public GameObject DemoButton;
	public GameObject SPButtonGraphic;
	public GameObject VSButtonGraphic;
	public GameObject DemoButtonGraphic;
	public GameObject MusicToggleButton;
	public GameObject AIToggleButton;
	public GameObject MusicToggleButtonText;
	public GameObject AIToggleButtonText;
	public GameObject QuitButtonPreGame;
	public GameObject StartButtonText;
	public GameObject[] CustomisationButtons;
	public GameObject Pausedgui;
	public GameObject CustomiseGUI;
	
		
	//PAUSE MENU//
	public GameObject MenuButton;
	
	//POST GAME MENU//
	public GameObject RematchButton;
	
	//WinCounters
	public int BlueWins = 0;
	public int RedWins = 0;
	public GameObject RedScore;
	public GameObject BlueScore;
	
	//Globals
	public bool Paused = false;
	public bool musicOn = false;
	public bool CustomisedYet = false;
	
	//Win/Lose Booleans
	public bool RedLose = false;
	public bool RedWin = false;
	public bool BlueLose = false;
	public bool BlueWin = false;
	
	public GameObject RedWinsGUI;
	public GameObject BlueWinsGUI;
	
	//Audio
	public GameObject BoomBox;
	
	public bool AIsettingsSent = false;
	public bool AIsettingNormal = true;
	public string NormalText = "AI : Normal";
	public string CheatText = "AI : Cheat";
	public string currenttext;
	
	public string AudioOnText = "Music : On";
	public string AudioOffText = "Music : Off";
	public string currentaudiostatetext;
	
	public Material HEAD2HEADActive;
	public Material HEADVSCPUActive;
	public Material CPUVSCPUActive;
	public Material HEAD2HEADInActive;
	public Material HEADVSCPUInActive;
	public Material CPUVSCPUInActive;
	
	public int FirstBoot = 0;
	
	
	//DEBUG ONLY Game State Changers for testing in inpector view
	public GameState debuggamestateSetter;
	
	// Use this for initialization
	void Start () 
	{
		FirstBoot = PlayerPrefs.GetInt("FirstBoot");
		if(FirstBoot==0)
		{
			//FIRST RUN. Define settings for the menus and their states for default params
			PlayerPrefs.SetInt("MusicOn",1);
			FirstBoot = 1;
			PlayerPrefs.SetInt("FirstBoot",1);
		}
			CustomiseGUI.active = true;
		if(PlayerPrefs.GetInt("MusicOn") == 1)
		{
			BoomBox.audio.enabled = true;
			currentaudiostatetext = AudioOnText;
			PlayerPrefs.SetInt("MusicOn",1);
			musicOn = true;
		}
		else{currentaudiostatetext = AudioOffText;PlayerPrefs.SetInt("MusicOn",0);musicOn = false;}
		RedPaddle = GameObject.FindGameObjectWithTag("RedPaddle");
		BluePaddle = GameObject.FindGameObjectWithTag("BluePaddle");
		RedWinsGUI = GameObject.FindGameObjectWithTag("REDWINSGUI");
		BlueWinsGUI= GameObject.FindGameObjectWithTag("BLUEWINSGUI");
		PlayerPrefs.SetInt("RedPlayerScore",0);
		PlayerPrefs.SetInt("BluePlayerScore",0);
		gametype = GameType.HumanVsCPU;
		SetCustomisationMenuOff();
		AIbuttonText();
		AudioButtonText();
		StartButtonText.active = false;
		AIToggleButtonText.gameObject.GetComponent<TextMesh>().text = currenttext;
		MusicToggleButtonText.gameObject.GetComponent<TextMesh>().text = currentaudiostatetext;
		SetGameModeButtonMaterials();
		RedWinsGUI.guiTexture.enabled = false;
		BlueWinsGUI.guiTexture.enabled = false;
		foreach (GameObject gameObject in Joysticks)
		{
			gameObject.active = false;
		}
	}
	
	void SetCustomisationMenuOff()
	{
		for(int i = 0 ; i<CustomisationButtons.Length;i++)
		{
			CustomisationButtons[i].SetActiveRecursively(false);
		}
	}
	
	void SetCustomisationMenuOn()
	{
		for(int i = 0 ; i<CustomisationButtons.Length;i++)
		{
			CustomisationButtons[i].SetActiveRecursively(true);
		}
	}
	
	void SetGameModeButtonMaterials()
	{
		switch (gametype)
			{
				case GameType.Head2Head:
						SPButtonGraphic.renderer.material = HEADVSCPUInActive;
						VSButtonGraphic.renderer.material = HEAD2HEADActive;
						DemoButtonGraphic.renderer.material = CPUVSCPUInActive;
					break;
				case GameType.HumanVsCPU:
						SPButtonGraphic.renderer.material = HEADVSCPUActive;
						VSButtonGraphic.renderer.material = HEAD2HEADInActive;
						DemoButtonGraphic.renderer.material = CPUVSCPUInActive;			
					break;
				case GameType.CPUVSCPU:
						SPButtonGraphic.renderer.material = HEADVSCPUInActive;
						VSButtonGraphic.renderer.material = HEAD2HEADInActive;
						DemoButtonGraphic.renderer.material = CPUVSCPUActive;	
					break;
			}
	}
	
	void SetGameMode(int gt)
	{
		if(gt == 1)//SP
		{gametype = GameType.HumanVsCPU;}
		if(gt == 2)//VS
		{gametype = GameType.Head2Head;}
		if(gt == 0)//DEMO
		{gametype = GameType.CPUVSCPU;}
		SetGameModeButtonMaterials();
		
	}
	

	/*void OnGUI()
	{
	  // Change the font size for the label
           	switch(gameState)
		{
			case GameState.PreGame:
			{
			GUI.skin = GUISKIN;
			PreGameWindow = GUI.Window (0, PreGameWindow, PreGameWindowFunction, "Game Setup");
			break;
			}
			case GameState.GamePlay:
			{
			GUI.skin = GUISKIN;
			
			if(Paused == true)
			{
			PauseWindow = GUI.Window (0, PauseWindow, PauseWindowFunction, "PAUSED!");
			}
			break;
			}
			case GameState.PostGame:
			{
			GUI.skin = GUISKIN2;
			PostGameWindow = GUI.Window (1, PostGameWindow, PostGameWindowFunction, "Game Over, Yeah!");
			break;
			}
		}
	}
	*/
	
	
	void PostGameWindowFunction()
	{
		
			/*GUILayout.Label("Red Wins:",GUILayout.Width(Screen.width*0.2f));
			GUILayout.Label(""+RedWins,GUILayout.Width(Screen.width*0.2f));
	
			GUILayout.Label("Blue Wins:",GUILayout.Width(Screen.width*0.2f));
			GUILayout.Label(""+BlueWins,GUILayout.Width(Screen.width*0.2f));*/
		
	}
	
	void PauseWindowFunction()
	{
			/*
				GUILayout.Label("Press Back to resume",GUILayout.Width(150));
			*/
	}
	
	void Menu(){Time.timeScale = 1;
			Paused = false;
		    NewGame();}

	void AIToggle()
	{
		if(AIsettingNormal == true){AIsettingNormal = false;}
						else if(AIsettingNormal == false){AIsettingNormal = true;}
						if(AIsettingNormal == true)
						{
							RedPaddle.SendMessage("AIModeSet",false);
							BluePaddle.SendMessage("AIModeSet",false);
							AIbuttonText();
							
						}
						else
						{
							RedPaddle.SendMessage("AIModeSet",true);
							BluePaddle.SendMessage("AIModeSet",true);
							AIbuttonText();
						}
		AIToggleButtonText.gameObject.GetComponent<TextMesh>().text = currenttext;
	}
	
	void MusicToggle()
	{
		if(musicOn == true){musicOn = false;}
					else if(musicOn == false){musicOn = true;}
					if(musicOn == true)
					{
					BoomBox.audio.enabled = true;
					PlayerPrefs.SetInt("MusicOn",1);
					AudioButtonText();
					}
					else
					{
					BoomBox.audio.enabled = false;
					PlayerPrefs.SetInt("MusicOn",0);
					AudioButtonText();
					}
		MusicToggleButtonText.gameObject.GetComponent<TextMesh>().text = currentaudiostatetext;
	}
	
		
	void AIbuttonText()
	{
		if(AIsettingNormal)
		{
			currenttext = NormalText;
		}
		else{currenttext= CheatText;}
	}
	
	void AudioButtonText()
	{
		if(musicOn)
		{
			currentaudiostatetext = AudioOnText;
		}
		else{currentaudiostatetext = AudioOffText;}
	}
	
	void Customise()
		{
		SetCustomisationMenuOn();
		CustomisedYet = true;
		GameObject.FindGameObjectWithTag("Ball").SendMessage("DestroyBall");
		PlayerPrefs.SetInt("RedPlayerScore",0);
		PlayerPrefs.SetInt("BluePlayerScore",0);
		RedLose = false;
		RedWin = false;
		BlueLose = false;
		BlueWin = false;
		RedPaddle.transform.position = RedSpawn.transform.position;
		BluePaddle.transform.position = BlueSpawn.transform.position;
		CustomiseGUI.active = false;
		StartButtonText.active = true;
		RedPaddle.SendMessage("AIoff");
		BluePaddle.SendMessage("AIoff");
		PlayerPrefs.SetInt("JoyOn",0);
			
		}
	
	void StartGame()
	{
		PlayerPrefs.SetInt("JoyOn",1);
		SetCustomisationMenuOff();
		GameLogic.gameState = GameState.GamePlay;
		debuggamestateSetter = GameLogic.gameState;
		RedWinsGUI.guiTexture.enabled = false;
		BlueWinsGUI.guiTexture.enabled = false;
		CustomisedYet = false;
		foreach (GameObject gameObject in Joysticks)
		{
			gameObject.active = true;
		}
	}
	void NewGame()
	{
		StartButtonText.active = false;
		GameLogic.gameState = GameState.PreGame;
		gametype = GameType.HumanVsCPU;
		SetCustomisationMenuOff();
		SetGameModeButtonMaterials();
		debuggamestateSetter = GameLogic.gameState;
		RedLose = false;
		RedWin = false;
		BlueLose = false;
		BlueWin = false;
		AIsettingsSent = false;
		RedWinsGUI.guiTexture.enabled = false;
		BlueWinsGUI.guiTexture.enabled = false;
		PlayerPrefs.SetInt("RedPlayerScore",0);
		PlayerPrefs.SetInt("BluePlayerScore",0);
		CustomisedYet = false;
		CustomiseGUI.active = true;
		foreach (GameObject gameObject in Joysticks)
		{
			gameObject.active = false;
		}
	}
	
	public void RedWinSet()
	{
		RedWin = true;
		BlueLose = true;
	}
	public void BlueWinSet()
	{
		BlueWin = true;
		RedLose = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//Menu control
				if(Application.isEditor && Input.GetMouseButtonUp(0))
				{
				Vector2 mousepos;
				mousepos.x = Input.mousePosition.x;
				mousepos.y = Input.mousePosition.y;
				Ray ray = Camera.main.ScreenPointToRay(mousepos);
					            RaycastHit hit ;
					            if (Physics.Raycast (ray, out hit , 8 )) 
								{
					                 hit.transform.SendMessage("Selected");
					            }
				}
  				 foreach (Touch touch in Input.touches) 
					{
					     if (touch.phase == TouchPhase.Began) 
							{
								Ray ray = Camera.main.ScreenPointToRay(touch.position);
					            RaycastHit hit ;
					            if (Physics.Raycast (ray, out hit,8)) 
								{
					                 hit.transform.SendMessage("Selected");
					            }
					        }
					 }
		
		//GUI MENUS
		switch(gameState)
		{
			case GameState.PreGame:
			{
			if(CustomisedYet == false)
			{
				StartGameButton.SetActiveRecursively(true);
				StartButtonText.active = false;
				VSButton.SetActiveRecursively(true);
				SPButton.SetActiveRecursively(true);
				DemoButton.SetActiveRecursively(true);
				MusicToggleButton.SetActiveRecursively(true);
				AIToggleButton.SetActiveRecursively(true);
				QuitButtonPreGame.SetActiveRecursively(true);
				MenuButton.SetActiveRecursively(false);
				RematchButton.SetActiveRecursively(false);
				Pausedgui.SetActiveRecursively(false);
			}
			else if (CustomisedYet == true)
			{
				StartGameButton.SetActiveRecursively(true);
				VSButton.SetActiveRecursively(false);
				SPButton.SetActiveRecursively(false);
				DemoButton.SetActiveRecursively(false);
				MusicToggleButton.SetActiveRecursively(false);
				AIToggleButton.SetActiveRecursively(false);
				QuitButtonPreGame.SetActiveRecursively(false);
				MenuButton.SetActiveRecursively(false);
				RematchButton.SetActiveRecursively(false);
				Pausedgui.SetActiveRecursively(false);
				for(int i = 0; i<Joysticks.Length; i++)
					{
						Joysticks[i].gameObject.active = false;
					}
			}
			
				//other GFX false
			break;
			}
			case GameState.GamePlay:
			{
			StartGameButton.SetActiveRecursively(false);
			VSButton.SetActiveRecursively(false);
			SPButton.SetActiveRecursively(false);
			DemoButton.SetActiveRecursively(false);
			MusicToggleButton.SetActiveRecursively(false);
			AIToggleButton.SetActiveRecursively(false);
			QuitButtonPreGame.SetActiveRecursively(false);
			MenuButton.SetActiveRecursively(false);
				RematchButton.SetActiveRecursively(false);
			Pausedgui.SetActiveRecursively(false);
			if(Paused == true)
			{
				Pausedgui.SetActiveRecursively(true);
			MenuButton.SetActiveRecursively(true);
				RematchButton.SetActiveRecursively(false);
				//other GFX false
			QuitButtonPreGame.SetActiveRecursively(true);
			}
			break;
			}
			case GameState.PostGame:
			{
				RematchButton.SetActiveRecursively(true);
			MenuButton.SetActiveRecursively(false);
			Pausedgui.SetActiveRecursively(false);
			//other GFX false
			StartGameButton.SetActiveRecursively(false);
			VSButton.SetActiveRecursively(false);
			SPButton.SetActiveRecursively(false);
			DemoButton.SetActiveRecursively(false);
			MusicToggleButton.SetActiveRecursively(false);
			AIToggleButton.SetActiveRecursively(false);
			QuitButtonPreGame.SetActiveRecursively(true);
			break;
			}
		}
		
		
		
		
		//debug setters ////////////////////
		/*gameState = debuggamestateSetter;
		playerTurn = debugplayerTurnSetter;*/
		////////////////////////////////////
		
		switch(gameState)
		{
			case GameState.PreGame:
			{
				//
				if(CustomisedYet == false)
					{	
				RedPaddle.SendMessage("AIon");
				BluePaddle.SendMessage("AIon");
					}		
				break;
			}
			case GameState.GamePlay:
			{
			if(AIsettingsSent == false)
			{
				switch(gametype)
				{
				case GameType.Head2Head:
				{
				RedPaddle.SendMessage("AIoff");
				BluePaddle.SendMessage("AIoff");
				AIsettingsSent=true;
				break;
				}
				case GameType.HumanVsCPU:
				{
				RedPaddle.SendMessage("AIoff");
				BluePaddle.SendMessage("AIon");
				AIsettingsSent=true;
				break;
				}
				case GameType.CPUVSCPU:
				{
				RedPaddle.SendMessage("AIon");
				BluePaddle.SendMessage("AIon");
				AIsettingsSent=true;
				break;
				}
				}
			}
				
				
				if(RedLose ==true || BlueLose==true)
				{
					if(RedLose == true)
					{
						BlueWin = true;
						BlueWins++;
					}
					else if(BlueLose == true)
					{
						RedWin = true;
						RedWins++;
					}
					RedLose = false;
					BlueLose = false;
					gameState = GameState.PostGame;
				}
			
				if(Input.GetKeyDown(KeyCode.Escape)) //PAUSE
				{
					if(Paused==false){Time.timeScale = 0.0f;Paused = true;}
					else if(Paused==true){Time.timeScale = 1;Paused = false;}
					if(Paused == true)
					{
					
					//ShowPauseMenu
					//if(Input.GetKeyDown(KeyCode.Escape)){Application.Quit();}
					
					}
					if(Paused == false)
					{
					//remove pause menu
					
					
					}
					//pause
					
			    }
				
				break;
			}
			case GameState.PostGame:
			{
			
				if(BlueWin == true)
				{
					RedWinsGUI.guiTexture.enabled = false;
					BlueWinsGUI.guiTexture.enabled = true;
				}
				else if(RedWin == true)
				{
					RedWinsGUI.guiTexture.enabled = true;
					BlueWinsGUI.guiTexture.enabled = false;
				}
			
			
				break;
			}
		}
	}
}
