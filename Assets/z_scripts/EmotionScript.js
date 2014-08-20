#pragma strict

public var target : GameObject;
public var enemyGoal: GameObject;
public var mouth : GameObject;
public var angrymouth : GameObject;
public var eyebrows : GameObject[];
public var maxmouthY : float;
public var currentmouthY : float;
public var expressionspeed : float;
public var defaultTarget : GameObject;
public var targetmouthY : float;
public var angryTime : float;
public var opponent : GameObject;
function Start () {
maxmouthY = 1f;
currentmouthY = 0;
expressionspeed = 0.02;
targetmouthY = 0;
}

function SetTargetBall(ball : GameObject)
{
target = ball;
}

function Update () {


if(target == null)
{
target = defaultTarget;
targetmouthY = 0;
}

if(Vector3.Distance(transform.position,opponent.transform.position)<2.5f)
{

for (var i = 0;i<eyebrows.Length;i++)
{
eyebrows[i].renderer.enabled = true;
}
angrymouth.renderer.enabled = true;
mouth.renderer.enabled = true;
angryTime = 3;

}
if(angryTime>0)
{
angryTime--;
}
if(angryTime<=0)
{
for (var j = 0;j<eyebrows.Length;j++)
{
eyebrows[j].renderer.enabled = false;
}
angrymouth.renderer.enabled = false;
mouth.renderer.enabled = true;
}

// first, find the distance from the center of the eye to the target
var distanceToTarget : float = Vector3.Distance(target.transform.position,enemyGoal.transform.position);

//Debug.Log(distanceToTarget);


if(distanceToTarget < 4)
{
 targetmouthY = maxmouthY;
  if(distanceToTarget>2){expressionspeed=0.08;}
}

else if(distanceToTarget > 6 && distanceToTarget < 14)
{
targetmouthY = 0;
}
else if (distanceToTarget > 14 && distanceToTarget < 25)
{
targetmouthY = -maxmouthY;
if(distanceToTarget>2){expressionspeed=0.08;}
}

if(expressionspeed >0.2){expressionspeed-= 0.001;};

currentmouthY = Mathf.Lerp(currentmouthY,targetmouthY,expressionspeed);

mouth.transform.localScale.y = currentmouthY;

}