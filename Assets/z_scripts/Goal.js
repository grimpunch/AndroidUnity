#pragma strict


enum PlayerColourGoal {Red,Blue}
var PlayerColourGoalState : PlayerColourGoal;
var BallSpawn : GameObject;
var textureAlpha : float;
var alphatemp : float;
var flashtime : float;
public var ScoreParticle : GameObject;

function Start () {

textureAlpha = this.renderer.material.color.a;

switch(PlayerColourGoalState)
	{
	case PlayerColourGoal.Red:
	
		PlayerPrefs.SetInt("RedPlayerScore",0);
		break;
	case PlayerColourGoal.Blue:
	
		PlayerPrefs.SetInt("BluePlayerScore",0);
		break;
	}

}

function OnTriggerStay(other : Collider)
{
	if(other.gameObject.tag == "Ball")
	{
		switch(PlayerColourGoalState)
		{
		case PlayerColourGoal.Red:
			PlayerPrefs.SetInt("BluePlayerScore",PlayerPrefs.GetInt("BluePlayerScore")+1);
			break;	
		case PlayerColourGoal.Blue:
		
			PlayerPrefs.SetInt("RedPlayerScore",PlayerPrefs.GetInt("RedPlayerScore")+1);
			break;
		}
		this.renderer.material.color.a = alphatemp;
		Instantiate(ScoreParticle,transform.position,transform.rotation);
		BallSpawn.SendMessage("BallLost");
		Destroy(other.gameObject);
	}
	else
	{
	
	}
	
}



function Update () {

	if(this.renderer.material.color.a != textureAlpha)
	{
	this.renderer.material.color.a -= flashtime;
	if(this.renderer.material.color.a < textureAlpha)
	{this.renderer.material.color.a = textureAlpha;}
	
	}
	

}