#pragma strict


public var target : GameObject;
public var eye : GameObject;
public var pupil : GameObject;
public var eyeRadius : float;
public var defaultTarget : GameObject;



function Start () {
pupil = this.gameObject;

}

function SetTargetBall(ball : GameObject)
{
target = ball;
}




function Update () {

if(target == null)
{
target = defaultTarget;
}

// first, find the distance from the center of the eye to the target
var distanceToTarget : Vector3 = target.transform.position - eye.transform.position;

// clamp the distance so it never exceeds the size of the eyeball
distanceToTarget = Vector3.ClampMagnitude( distanceToTarget, eyeRadius );

// place the pupil at the desired position relative to the eyeball
var finalPupilPosition : Vector3 = eye.transform.position + distanceToTarget;
finalPupilPosition.z = transform.position.z; 
pupil.transform.position = finalPupilPosition;

}