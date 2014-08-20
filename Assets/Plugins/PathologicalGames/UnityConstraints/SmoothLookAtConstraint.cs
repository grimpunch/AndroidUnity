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
[AddComponentMenu("Path-o-logical/Constraints/Look At - Smooth")]
public class SmoothLookAtConstraint : LookAtConstraint
{
    public enum INTERP_OPTIONS { Linear, Spherical, SphericalLimited }
    public INTERP_OPTIONS interpolation = INTERP_OPTIONS.Spherical;
    public float speed = 1;

    public enum OUTPUT_OPTIONS { WorldAll,
                                 WorldX, WorldY, WorldZ,
                                 LocalX, LocalY, LocalZ }
    public OUTPUT_OPTIONS output = OUTPUT_OPTIONS.WorldAll;

    // Just so we don't have to make a copy of identity every frame
    private Quaternion newRot = Quaternion.identity;

    // Reused every frame (probably overkill, but can't hurt...)
    private Quaternion lookRot;
    private Quaternion usrLookRot;
    private Quaternion curRot;
    private Vector3 angles;


    /// <summary>
    /// Runs each frame while the constraint is active
    /// </summary>
    protected override void OnConstraintUpdate()
    {
        // Note: Do not run base.OnConstraintUpdate. It is not implimented

        // Note: All variables are cached in the class, but I only used the
        //       'this' keyword for members which are interacted with outside
        //       of this method just for visual clarity.

        var look = this.lookVect;  // Property, so cache result
        if (look == Vector3.zero) return;

        lookRot = Quaternion.LookRotation(look, this.upVect);
        usrLookRot = this.GetUserLookRotation(lookRot);

        this.OutputTowards(usrLookRot);
    }

    /// <summary>
    /// Runs when the noTarget mode is set to ReturnToDefault
    /// </summary>
    protected override void NoTargetDefault()
    {
        this.OutputTowards(Quaternion.identity);
    }

    /// <summary>
    /// Runs when the noTarget mode is set to ReturnToDefault
    /// </summary>
    private void OutputTowards(Quaternion destRot)
    {
        curRot = this.xform.rotation;
        switch (this.interpolation)
        {
            case INTERP_OPTIONS.Linear:
                newRot = Quaternion.Lerp(curRot, destRot, Time.deltaTime * this.speed);
                break;

            case INTERP_OPTIONS.Spherical:
                newRot = Quaternion.Slerp(curRot, destRot, Time.deltaTime * this.speed);
                break;

            case INTERP_OPTIONS.SphericalLimited:
                newRot = Quaternion.RotateTowards(curRot, destRot, this.speed * Time.timeScale);
                break;

        }

        this.xform.rotation = newRot;

        #region OUTPUT_OPTIONS
        switch (this.output)
        {
            case OUTPUT_OPTIONS.WorldAll:
                // Already done
                break;

            case OUTPUT_OPTIONS.WorldX:
                angles = this.xform.eulerAngles;
                angles.y = 0;
                angles.z = 0;
                this.xform.eulerAngles = angles;
                break;

            case OUTPUT_OPTIONS.WorldY:
                angles = this.xform.eulerAngles;
                angles.x = 0;
                angles.z = 0;
                this.xform.eulerAngles = angles;
                break;

            case OUTPUT_OPTIONS.WorldZ:
                angles = this.xform.eulerAngles;
                angles.x = 0;
                angles.y = 0;
                this.xform.eulerAngles = angles;
                break;

            case OUTPUT_OPTIONS.LocalX:
                angles = this.xform.localEulerAngles;
                angles.y = 0;
                angles.z = 0;
                this.xform.localEulerAngles = angles;
                break;

            case OUTPUT_OPTIONS.LocalY:
                angles = this.xform.localEulerAngles;
                angles.x = 0;
                angles.z = 0;
                this.xform.localEulerAngles = angles;
                break;

            case OUTPUT_OPTIONS.LocalZ:
                angles = this.xform.localEulerAngles;
                angles.x = 0;
                angles.y = 0;
                this.xform.localEulerAngles = angles;
                break;
        }
        #endregion OUTPUT_OPTIONS

    }

}
