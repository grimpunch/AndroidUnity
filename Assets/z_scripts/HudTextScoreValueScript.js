#pragma strict


enum PlayerColourScore {Red,Blue}
var PlayerColourScoreState : PlayerColourScore;

var menu : GameObject;

function Start () {

GetComponent(TextMesh).text = "0";

}


function Update () {
	switch(PlayerColourScoreState)
	{
	case PlayerColourScore.Red:
	
		if(PlayerPrefs.GetInt("RedPlayerScore")<9999999)
		{
		GetComponent(TextMesh).text = ""+ PlayerPrefs.GetInt("RedPlayerScore");
		}
		if(PlayerPrefs.GetInt("RedPlayerScore")>=5)
		{
		menu.SendMessage("RedWinSet",SendMessageOptions.RequireReceiver);
		}
		break;
	case PlayerColourScore.Blue:
	
		if(PlayerPrefs.GetInt("BluePlayerScore")<9999999)
		{
		GetComponent(TextMesh).text = ""+ PlayerPrefs.GetInt("BluePlayerScore");
		}
		if(PlayerPrefs.GetInt("BluePlayerScore")>=5)
		{
		menu.SendMessage("BlueWinSet",SendMessageOptions.RequireReceiver);
		}	
		break;
	}
	

}