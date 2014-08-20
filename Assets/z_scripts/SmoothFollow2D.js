
#pragma strict

var target : Transform;
var smoothTime = 0.3;
var shotRange = 2.3;

private var thisTransform : Transform;
private var velocity : Vector2;
public var AI : boolean;
enum AIType {normal,cheat}
var AIMode : AIType;
public var toRespawn : boolean;
public  var ball : GameObject;
public var SPAWN : GameObject;
public var hammerControl : GameObject;
public var joystick : GameObject;
public var opponent : GameObject;
public var bounceTimer: float;
public var BounceTimerSet:float;
public var enemyhammer : GameObject;


function Start()
{
	BounceTimerSet = 2;
	
	thisTransform = transform;
	if(AI == false)
	{
	joystick.active = true;
	PlayerPrefs.SetInt("AI",0);
	}
	else{
	joystick.active = false;
	PlayerPrefs.SetInt("AI",1);
	}
}
function AIon()
{
AI = true;

}
function AIoff()
{
AI = false;
toRespawn = true;
}

function AIModeSet(ai : boolean)
{
if(ai == false)
this.AIMode = AIType.normal;

if(ai == true)
this.AIMode = AIType.cheat;
}

function AIBallLost()
{
target = SPAWN.transform;
if(AI == false)
	return;
//joystick.active = false;
}

function OnCollisionEnter(collision:Collision)
{
	if(collision.gameObject == enemyhammer)
	{
		velocity.Set(velocity.x - velocity.x*4,velocity.y- velocity.y*4);
	 	//iTween.ShakePosition(gameObject,Vector3(2,0,0),2);
				/* if(this.gameObject.tag == "BluePaddle"){transform.position.x += 0.5f;}
				if(this.gameObject.tag == "RedPaddle"){transform.position.x -= 0.5f;}*/
	}
}

function Respawn()
{
	thisTransform.position = SPAWN.transform.position;
	
	//ball.SendMessage("DestroyBall",SendMessageOptions.RequireReceiver); 
	joystick.active = true;
	PlayerPrefs.SetInt("AI",0);
	PlayerPrefs.SetInt("RedPlayerScore",0);
			PlayerPrefs.SetInt("BluePlayerScore",0);
			hammerControl.hingeJoint.useMotor = false;
	//AIBallLost();
	toRespawn = false;
}

function Update() 
{
	if(transform.position.x < -11.5 || transform.position.x > 11.5)
	{
	transform.position = SPAWN.transform.position;
	}
	if(transform.position.y < -8 || transform.position.y > 8)
	{
	transform.position = SPAWN.transform.position;
	}
	
	if(PlayerPrefs.GetInt("JoyOn")==0){joystick.active = false;};
	if(AI == false)
	{
	if(PlayerPrefs.GetInt("JoyOn")==1){joystick.active = true;};
	PlayerPrefs.SetInt("AI",0);
	
		if(toRespawn == true)
		{
			Respawn();
			
		}
		
		//hammerControl.rigidbody.velocity.Set(0,0,0);
	}
	else{
	joystick.active = false;
	//PlayerPrefs.SetInt("AI",1);
	}
	if(AI == false)
	return;
	//
	if( PlayerPrefs.GetInt("BallsInPlay") > 0)
	{
		target = GameObject.FindGameObjectWithTag("Ball").transform;
		ball = GameObject.FindGameObjectWithTag("Ball");
	}
	else
	{
		target = SPAWN.transform;
	}
	if(ball == null)
	return;
	
	
	
	
	//SPEED LIMIT TEST
	switch(AIMode)
	{
		case AIType.normal:
			smoothTime = Vector3.Distance(transform.position,ball.transform.position)/1.5f;
			//Randomisation
			if(velocity.x > 3){velocity.x = 2.7f;}
			if(velocity.y > 2){velocity.y = 2.0f;}
			velocity.x += Random.RandomRange(-0.15f,0.15f);
			velocity.y += Random.RandomRange(-0.15f,0.15f);		
			break;
		
		case AIType.cheat:
			smoothTime = 1;
			break;
		
	}
	
		
			
			
	if(ball.transform.position.y<0 && transform.position.y > - 4.4f)
		{
		
		thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
		target.position.x, velocity.x, smoothTime);
		thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
		target.position.y+1.8f, velocity.y, smoothTime);
		
		
			if(ball.transform.position.x < 0 && transform.position.x > -8)
			{
			thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
			target.position.x+1.9f, velocity.x, smoothTime);
			thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
			target.position.y, velocity.y, smoothTime);
			}
			if(ball.transform.position.x > 0 && transform.position.x < 8)
			{
			thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
			target.position.x-1.9f, velocity.x, smoothTime);
			thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
			target.position.y, velocity.y, smoothTime);
			}
		}
	if(ball.transform.position.y>0 && transform.position.y < 4.4f)
		{
		
		thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
		target.position.x, velocity.x, smoothTime);
		thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
		target.position.y-1.8f, velocity.y, smoothTime);
		if(ball.transform.position.x < 0 && transform.position.x > -8)
			{
			thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
			target.position.x +1.9f, velocity.x, smoothTime);
			thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
			target.position.y, velocity.y, smoothTime);
			}
			if(ball.transform.position.x > 0 && transform.position.x < 8)
			{
			thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
			target.position.x-1.9f, velocity.x, smoothTime);
			thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
			target.position.y, velocity.y, smoothTime);
			}
		}
		
	if(transform.position.y < - 4.4f){transform.position.y+= 0.01f;};
	if(transform.position.y >  4.4f){transform.position.y-= 0.01f;};
	
	
		if(Vector3.Distance(transform.position,opponent.transform.position) <= 2.0f)
		{
			 if(bounceTimer <0 || bounceTimer == -1)
			 {
			 velocity.Set(velocity.x - velocity.x*4,velocity.y- velocity.y*4);
			 
				 if(this.gameObject.tag == "BluePaddle"){transform.position.x += 0.3f;}
				if(this.gameObject.tag == "RedPaddle"){transform.position.x -= 0.3f;}
				bounceTimer = BounceTimerSet;
			 }
		}
		
		if(bounceTimer>0)
		{
		bounceTimer--;
		}
		else{bounceTimer = -1;}
		
		//AI Shooting
		
		if(Vector3.Distance(thisTransform.position,target.position)<shotRange)
		{
			if(this.gameObject.tag == "BluePaddle")
			{
				//begin checks to see which way to swing hammer
				if(target.position.y > thisTransform.position.y)	//Ball is above Blue AI player
				{
					hammerControl.hingeJoint.motor.targetVelocity = 1000;
					hammerControl.hingeJoint.useMotor = true;
				}
				if(target.position.y < thisTransform.position.y)	//Ball is below Blue AI player
				{
					hammerControl.hingeJoint.motor.targetVelocity = -1000;
					hammerControl.hingeJoint.useMotor = true;
				}		
				if(target.position.x > thisTransform.position.x)	//Ball is right of Blue AI player
				{
					if(target.position.x > 8 && (target.position.y <= 4 && target.position.y >= -4))
					{
					
					thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
					target.position.x + 2.9f, velocity.x, smoothTime);
					thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
					target.position.y + 4.9f, velocity.y, smoothTime);
					hammerControl.hingeJoint.motor.targetVelocity = -150;
					}
					else{hammerControl.hingeJoint.motor.targetVelocity = -500;}
					hammerControl.hingeJoint.useMotor = true;
					
				}
				if(target.position.x < thisTransform.position.x)	//Ball is left of Blue AI player
				{
					hammerControl.hingeJoint.motor.targetVelocity = 1000;
					
					hammerControl.hingeJoint.useMotor = true;
				}
				if(hammerControl.hingeJoint.useMotor == true)
				{
				if(hammerControl.audio.enabled==true){hammerControl.audio.Play();}
				}
			}
		
		
			if(this.gameObject.tag == "RedPaddle")
			{
			//begin checks to see which way to swing hammer
				if(target.position.y > thisTransform.position.y)	//Ball is above Red AI player
				{
					hammerControl.hingeJoint.motor.targetVelocity = 1000;
					hammerControl.hingeJoint.useMotor = true;
				}
				if(target.position.y < thisTransform.position.y)	//Ball is below Red AI player
				{
					hammerControl.hingeJoint.motor.targetVelocity = -1000;
					hammerControl.hingeJoint.useMotor = true;
				}		
				if(target.position.x > thisTransform.position.x)	//Ball is Right of Red AI player
				{
					hammerControl.hingeJoint.motor.targetVelocity = -1000;
					hammerControl.hingeJoint.useMotor = true;
				}
				if(target.position.x < thisTransform.position.x)	//Ball is Left of Red AI player
				{
					
					if(target.position.x < -8 && (target.position.y <= 4 && target.position.y >= -4))
					{
					
					thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
					target.position.x-2.9f, velocity.x, smoothTime);
					thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
					target.position.y-4.9f, velocity.y, smoothTime);
					hammerControl.hingeJoint.motor.targetVelocity = 150;
					
					}
					else{hammerControl.hingeJoint.motor.targetVelocity = 500;}
					
					hammerControl.hingeJoint.useMotor = true;
				}
				if(hammerControl.hingeJoint.useMotor == true)
				{
				if(hammerControl.audio.enabled==true){hammerControl.audio.Play();}
				}
			}
		
		}
		else
		{
	
		if(hammerControl.audio.enabled==true){hammerControl.audio.Stop();}
				
		hammerControl.hingeJoint.useMotor = false;
		
		}
	
}