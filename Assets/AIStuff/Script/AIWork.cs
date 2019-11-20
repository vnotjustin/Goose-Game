﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWork : MonoBehaviour {
    public AIDestination Destination;
    [Space]
    public float StartDelay;
    public string StartAnim;
    [Space]
    public bool RotationAnim;
    [Space]
    public float PRDelay;
    public string PRAnim;
    [Space]
    public float EndDelay;
    public string EndAnim;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void OnStart(AIWork LastWork)
    {

    }

    public virtual void OnEnd()
    {

    }

    public virtual Vector3 GetTargetPosition()
    {
        return Destination.GetTargetPosition();
    }
}

public enum AIWorkState
{
    Start,
    Rotate,
    PostRotate,
    Move,
    End
}