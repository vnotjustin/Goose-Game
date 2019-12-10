using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWork : MonoBehaviour {
    public AIDestination Destination;
    public Item TargetItem;
    [HideInInspector]
    public float CurrentTime;
    [HideInInspector]
    public bool Counting;
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
    public bool EndRotationLock;
    [Space]
    public AIWork NextWork;
    public bool Interrupted;

    // Start is called before the first frame update
    public virtual void Start()
    {
        CurrentTime = 20f;
        Counting = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Counting && CurrentTime <= 0f && AIControl.Main.CurrentWork == this)
            AIControl.Main.EndWork();
        if (Counting)
            CurrentTime -= Time.deltaTime;
    }

    public virtual void OnStart(AIWork LastWork)
    {
        CurrentTime = 20f;
        Counting = true;
    }

    public virtual void OnPreEnd()
    {

    }

    public virtual void OnEnd()
    {
        if (NextWork)
            AIControl.Main.SetWork(NextWork);
        Counting = false;
        CurrentTime = 20f;
    }

    public virtual Vector3 GetTargetPosition()
    {
        return Destination.GetTargetPosition();
    }

    public virtual bool OutRange()
    {
        return Destination.OutRange();
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
