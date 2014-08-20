/// <Licensing>
/// � 2011 (Copyright) Path-o-logical Games, LLC
/// Licensed under the Unity Asset Package Product License (the "License");
/// You may not use this file except in compliance with the License.
/// You may obtain a copy of the License at: http://licensing.path-o-logical.com
/// </Licensing>
using UnityEngine;
using System.Collections;

/// <summary>
///	Manages a "turntable" component which will rotate the owner in the Y access at
///	a constant speed while on. This will start itself and stop itself when enabled
///	or disabled as well as directly through the "on" bool property.
/// </summary>
[AddComponentMenu("Path-o-logical/Constraints/TurnTable")]
public class TurntableConstraint : ConstraintFrameworkBaseClass 
{
    /// <summary>
    /// The speed at which the turn table will turn
    /// </summary>
    public float speed = 1;

    /// <summary>
    /// The speed at which the turn table will turn
    /// </summary>
    public bool randomStart = true;


    protected override void Awake()
    {
        base.Awake();

        if (this.randomStart) 
            this.xform.Rotate(0, Random.value*360, 0);
    }


    /// <summary>
    /// Runs each frame while the constraint is active
    /// </summary>
    protected override void OnConstraintUpdate()
    {
        // Note: Do not run base.OnConstraintUpdate. It is not implimented

        // Add a rotation == this.speed per frame
        this.xform.Rotate(0, this.speed, 0);
    }
}
