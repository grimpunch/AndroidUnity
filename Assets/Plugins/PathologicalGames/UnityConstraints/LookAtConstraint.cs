/// <Licensing>
/// � 2011 (Copyright) Path-o-logical Games, LLC
/// Licensed under the Unity Asset Package Product License (the "License");
/// You may not use this file except in compliance with the License.
/// You may obtain a copy of the License at: http://licensing.path-o-logical.com
/// </Licensing>
using UnityEngine;
using System.Collections;

/// <summary>
///	The base class for all constraints that use a target and mode
/// </summary>
[AddComponentMenu("Path-o-logical/Constraints/Look At")]
public class LookAtConstraint : LookAtBaseClass 
{
    /// <summary>
    /// An optional target just for the upAxis. The upAxis may not point directly 
    /// at this. See the online docs for more info
    /// </summary>
    public Transform upTarget;


    // Get the lookVector
    protected Vector3 lookVect 
    {
        get { return this.target.position - this.xform.position;  }
    }

    // Get the upvector. Factors in any options.
    protected Vector3 upVect
    {
        get
        {
            Vector3 upVect;
            if (this.upTarget == null)
                upVect = Vector3.up;
            else
                upVect = this.upTarget.position - this.xform.position;

            return upVect;
        }
    }


    /// <summary>
    /// Runs each frame while the constraint is active
    /// </summary>
    protected override void OnConstraintUpdate()
    {
        // Note: Do not run base.OnConstraintUpdate. It is not implimented

        var lookRot = Quaternion.LookRotation(this.lookVect, this.upVect);
        this.xform.rotation = this.GetUserLookRotation(lookRot);
    }
}
