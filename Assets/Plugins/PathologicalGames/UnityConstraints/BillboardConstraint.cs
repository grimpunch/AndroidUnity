/// <Licensing>
/// � 2011 (Copyright) Path-o-logical Games, LLC
/// Licensed under the Unity Asset Package Product License (the "License");
/// You may not use this file except in compliance with the License.
/// You may obtain a copy of the License at: http://licensing.path-o-logical.com
/// </Licensing>
using UnityEngine;
using System.Collections;

/// <summary>
///	Billboarding is when an object is made to face a camera, so that no matter
///	where it is on screen, it looks flat (not simply a "look-at" constraint, it
///	keeps the transform looking parallel to the target - usually a camera). This
///	is ideal for sprites, flat planes with textures that always face the camera.
/// </summary>
[AddComponentMenu("Path-o-logical/Constraints/Billboard")]
public class BillboardConstraint : LookAtBaseClass 
{
    /// <summary>
    /// If false, the billboard will only rotate along the upAxis.
    /// </summary>
    public bool vertical = true;

    protected override void Awake() 
	{
        base.Awake();

        // Allow for a defaul if no target is given
        if (this.target == null)
            this.target = Camera.main.transform;  // Default
    }


    /// <summary>
    /// Runs each frame while the constraint is active
    /// </summary>
    protected override void OnConstraintUpdate()
    {
        // Note: Do not run base.OnConstraintUpdate. It is not implimented

        // This is based on the Unity wiki script which keeps this look-at
        //   vector parrallel with the camera, rather than right at the center
        Vector3 lookPos = this.xform.position + this.target.rotation * Vector3.back;
        Vector3 upVect = Vector3.up;

        // If the billboarding will happen vertically as well, then the upvector needs
        //   to be derrived from the target's up vector to remain parallel
        // If not billboarding vertically, then keep the defaul upvector and set the
        //   lookPos to be level with this object so it doesn't rotate in x or z
        if (this.vertical)
            upVect = this.target.rotation * Vector3.up;
        else
            lookPos.y = this.xform.position.y;

        // Get the final direction to look for processing with user input axis settings
        Vector3 lookVect = lookPos - this.xform.position;
        var lookRot = Quaternion.LookRotation(lookVect, upVect);
        this.xform.rotation = this.GetUserLookRotation(lookRot);

    }

}
