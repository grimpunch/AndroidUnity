/// <Licensing>
/// � 2011 (Copyright) Path-o-logical Games, LLC
/// Licensed under the Unity Asset Package Product License (the "License");
/// You may not use this file except in compliance with the License.
/// You may obtain a copy of the License at: http://licensing.path-o-logical.com
/// </Licensing>
using UnityEngine;
using System.Collections;

/// <description>
///	The base class for all constraints
/// </description>
public class ConstraintFrameworkBaseClass : MonoBehaviour
{

    // Cache...
    [HideInInspector]
    public Transform xform;   // Made public to share the cache when storing references

    /// <summary>
    /// Cache as much as possible before starting the co-routine
    /// </summary>
    protected virtual void Awake()
    {
        this.xform = this.transform;
    }

    /// <summary>
    /// Activate the constraint again if this object was disabled then enabled.
    /// Also runs immediatly after Awake()
    /// </summary>
    protected virtual void OnEnable()
    {
        this.InitConstraint();
    }

    /// <summary>
    /// Activate the constraint again if this object was disabled then enabled.
    /// Also runs immediatly after Awake()
    /// </summary>
    protected virtual void OnDisable()
    {
        this.StopCoroutine("Constrain");
    }

    /// <summary>
    /// Activate the constraint again if this object was disabled then enabled.
    /// Also runs immediatly after Awake()
    /// </summary>
    protected virtual void InitConstraint()
    {
        this.StartCoroutine("Constrain");
    }

    /// <summary>
    /// Runs as long as the component is active.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Constrain()
    {
        while (true)
        {
            // This will ensure the result is smooth by allowing the target
            //   to find it's final transform, at this frame, before this 
            //   object is set to it.
            yield return new WaitForEndOfFrame();

            this.OnConstraintUpdate();
        }
    }

    /// <summary>
    /// Impliment on child classes
    /// Runs each frame while the constraint is active
    /// </summary>
    protected virtual void OnConstraintUpdate()
    {
        throw new System.NotImplementedException();
    }
}


