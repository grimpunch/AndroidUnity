/// <Licensing>
/// � 2011 (Copyright) Path-o-logical Games, LLC
/// Licensed under the Unity Asset Package Product License (the "License");
/// You may not use this file except in compliance with the License.
/// You may obtain a copy of the License at: http://licensing.path-o-logical.com
/// </Licensing>
using UnityEngine;
using System.Collections;

/// <description>
///	Constrain this transform to a target's scale, rotation and/or translation.
/// </description>
[AddComponentMenu("Path-o-logical/Constraints/Transform (Postion, Rotation, Scale)")]
public class TransformConstraint : ConstraintBaseClass 
{    
    /// <summary>
    /// Option to match the target's position
    /// </summary>
    public bool constrainPosition = true;

    /// <summary>
    /// Option to match the target's rotation
    /// </summary>
    public bool constrainRotation = true;

    /// <summary>
    /// Option to match the target's scale. This is a little more expensive performance
    /// wise and not needed very often so the default is false.
    /// </summary>
    public bool constrainScale = false;


    // Cache...
    private Transform parXform;


    /// <summary>
    /// Cache as much as possible before starting the co-routine
    /// </summary>
    protected override void Awake() 
	{
        base.Awake();

        this.parXform = this.xform.parent;
    }


    /// <summary>
    /// Runs each frame while the constraint is active
    /// </summary>
    protected override void OnConstraintUpdate()
    {
        // Note: Do not run base.OnConstraintUpdate. It is not implimented

        if (this.constrainScale)
            this.SetWorldScale(target);

        if (this.constrainRotation)
            this.xform.rotation = this.target.rotation;

        if (this.constrainPosition)
            this.xform.position = this.target.position;
    }


    /// <summary>
    /// Runs when the noTarget mode is set to ReturnToDefault
    /// </summary>
    protected override void NoTargetDefault()
    {
        if (this.constrainScale)
            this.xform.localScale = Vector3.one;

        if (this.constrainRotation)
            this.xform.rotation = Quaternion.identity;

        if (this.constrainPosition)
            this.xform.position = Vector3.zero;
    }

    /// <summary>
    /// Sets this transform's scale to equal the target in world space.
    /// </summary>
    /// <param name="sourceXform"></param>
    public void SetWorldScale(Transform sourceXform)
    {
        // Make a reference transform and set it to the source's lossyScale, which
        //   is Unity's estimate of "world scale". Since this new object doesn't
        //   have a parent (it is in world space) set it to carry the lossyScale.
        GameObject refGO = new GameObject();   // Used to destroy later.
        Transform refXform = refGO.transform;

        // Cast the reference transform in to the space of the source Xform using
        //   Parenting, then cast it back to set.
        refXform.parent = sourceXform;
        refXform.localRotation = Quaternion.identity;  // Stablizes this solution
        refXform.localScale = Vector3.one;

        // Parent the reference transform to this object so they are in the same
        //   space, now we have a local scale to use.
        refXform.parent = this.parXform;

        // Set the scale now that both Transforms are in the same space
        this.xform.localScale = refXform.localScale;

        //Debug.DebugBreak();
        // Destroy the ref. Don't worry about Garbage Collection since this is a
        //   very simple object with no references - basically just a transform
        Destroy(refGO);
    }

}


