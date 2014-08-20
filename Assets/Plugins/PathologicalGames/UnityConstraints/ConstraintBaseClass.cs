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
public class ConstraintBaseClass : ConstraintFrameworkBaseClass
{
    /// <summary>
    /// The object to constrain to
    /// </summary>
    public Transform _target;  // For the inspector
    public Transform target
    {
        get 
        {
            if (this.noTargetMode == UnityConstraints.NO_TARGET_OPTIONS.SetByScript)
                return this.internalTarget;

            return this._target; 
        }

        set { this._target = value; }
    }


    // Used if the noTargetMode is set to SetByScript
    private Transform internalTarget;

    /// <summary>
    /// Set the position this constraint will use if the noTargetMode is set to 
    /// SetByScript. Usage is determined by script.
    /// </summary>
    public Vector3 position
    {
        get { return this.internalTarget.position; }
        set { this.internalTarget.position = value; }
    }

    /// <summary>
    /// Set the rotaion this constraint will use if the noTargetMode is set to 
    /// SetByScript. Usage is determined by script.
    /// </summary>
    public Quaternion rotation
    {
        get { return this.internalTarget.rotation; }
        set { this.internalTarget.rotation = value; }
    }

    /// <summary>
    /// Set the localScale this constraint will use if the noTargetMode is set to 
    /// SetByScript. Usage is determined by script.
    /// </summary>
    public Vector3 scale
    {
        get { return this.internalTarget.localScale; }
        set { this.internalTarget.localScale = value; }
    }

    /// <summary>
    /// Determines the behavior if no target is available
    /// </summary>
    // Public backing field for the inspector (The underscore will not show up)
    public UnityConstraints.NO_TARGET_OPTIONS _noTargetMode =
                                            UnityConstraints.NO_TARGET_OPTIONS.Error;
    public UnityConstraints.NO_TARGET_OPTIONS noTargetMode
    { 
        get { return this._noTargetMode; }
        set 
        {
            // Singleton & Just-in-time creation of internal target when set 
            //   through script. Set in Awake() for inspector use
            if (value == UnityConstraints.NO_TARGET_OPTIONS.SetByScript &&
                this.internalTarget == null)
                GenerateInternalTarget();

            this._noTargetMode = value; 
        }    
    }

    /// <summary>
    /// The current mode of the constraint.
    /// Setting the mode will start or stop the constraint coroutine, so if 
    /// 'alignOnce' is chosen, the constraint will align once then go to sleep.
    /// </summary>
    // Public backing field for the inspector (The underscore will not show up)
    public UnityConstraints.MODE_OPTIONS _mode = UnityConstraints.MODE_OPTIONS.Constrain;

    public UnityConstraints.MODE_OPTIONS mode
    {
        get { return this._mode; }
        set
        {
            this._mode = value;
            this.InitConstraint();
        }
    }


    protected override void Awake()
    {
        base.Awake();

        // Singleton for creation of internal target when set in the Inspector.
        //   (Set in the noTargetMode property setter when set through script.)
        if (this._noTargetMode == UnityConstraints.NO_TARGET_OPTIONS.SetByScript &&
                                                           this.internalTarget == null)
            this.GenerateInternalTarget();
    }

    // Clean-up
    private void OnDestroy()
    {
        // Destroy the internalTarget if one was created.
        if (this.internalTarget == null) Destroy(this.internalTarget);
    }

    /// <summary>
    /// Activate the constraint again if this object was disabled then enabled.
    /// Also runs immediatly after Awake()
    /// </summary>
    protected override sealed void InitConstraint()
    {
        switch (this.mode)
        {
            case UnityConstraints.MODE_OPTIONS.Off:
                break;

            case UnityConstraints.MODE_OPTIONS.Once:
                this.OnOnce();
                break;

            case UnityConstraints.MODE_OPTIONS.Constrain:
                this.StartCoroutine("Constrain");
                break;

        }
    }


    /// <summary>
    /// Runs as long as the component is active.
    /// </summary>
    /// <returns></returns>
    protected override sealed IEnumerator Constrain()
    {
        // Start on the next frame incase some init still needs to occur.
        yield return null;

        // While in Constrain mode handle this.target even if null.
        while (this.mode == UnityConstraints.MODE_OPTIONS.Constrain)
        {
            // If null, handle then continue to the next frame to test again.
            if (this.target == null) 
            {
                // While the target is null, handle using the noTargetMode options
                switch (noTargetMode)
                {
                    case UnityConstraints.NO_TARGET_OPTIONS.Error:
                        var msg = string.Format("No target provided. \n{0} on {1}",
                                                this.GetType().Name, 
                                                this.xform.name);

                        Debug.LogError(msg);  // Spams like Unity
                        yield return null;
                        continue;  // All done this frame, try again next.

                    case UnityConstraints.NO_TARGET_OPTIONS.DoNothing:
                        yield return null;  // Could omit. Kept for completeness
                        continue;  // All done this frame, try again next.

                    case UnityConstraints.NO_TARGET_OPTIONS.ReturnToDefault:
                        this.NoTargetDefault();
                        yield return null;
                        continue;  // All done this frame, try again next.

                    case UnityConstraints.NO_TARGET_OPTIONS.SetByScript:
                        // Handled as normal. Pass-through.
                        break;
                }
            }

            // Attempt the constraint...

            // This will ensure the result is smooth by allowing the target
            //   to find it's final transform, at this frame, before this 
            //   object is set to it.
            yield return new WaitForEndOfFrame();

            // Just incase the target is lost during the frame prior to this point.
            if (this.target == null) continue;

            // CONSTRAIN...
            this.OnConstraintUpdate();

        }
    }

    /// <summary>
    /// Runs when the noTarget mode is set to ReturnToDefault.
    /// Derrived constraints should just override and not run this
    /// </summary>
    protected virtual void NoTargetDefault()
    {
        // Use in child classes to do something when there is no target
    }

    /// <summary>
    /// Runs when the "once" option is chosen
    /// </summary>
    private void OnOnce()
    {
        this.OnConstraintUpdate();
    }

    /// <summary>
    /// Creates an empty game object for internal storage of transform information
    /// Runs the first time the SetByScript NoTargetMode is set.
    /// </summary>
    protected virtual void GenerateInternalTarget()
    {
        // Create a GameObject to use as the target for the constraint
        // Setting the name is really only for debugging and unexpected errors
        var go = new GameObject(this.name + "_InternalConstraintTarget");
        go.hideFlags = HideFlags.HideInHierarchy;  // Hide from the hierarchy
        this.internalTarget = go.transform;

        // Make a sibling so this object and the reference exist in the same space
        //   and reset
        this.internalTarget.position = this.xform.position;
        this.internalTarget.rotation = this.xform.rotation;
        this.internalTarget.localScale = this.xform.localScale;
    }
}
