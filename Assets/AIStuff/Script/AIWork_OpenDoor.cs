using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWork_OpenDoor : AIWork {
    public GameObject DirectionPoint;
    public AIWork PreWork;

    public override void Update()
    {
        base.Update();
        if (AIControl.Main.CurrentWork == this && AIControl.Main.CurrentWorkState == AIWorkState.End)
        {
            Vector3 a = DirectionPoint.transform.position;
            a.y = AIControl.Main.GetPosition().y;
            AIControl.Main.SetDirection(a - AIControl.Main.GetPosition());
        }
    }

    public override void OnStart(AIWork LastWork)
    {
        base.OnStart(LastWork);
        PreWork = LastWork;
    }

    public override void OnPreEnd()
    {
        base.OnPreEnd();
        DoorControl.Main.Open();
    }

    public override void OnEnd()
    {
        base.OnEnd();
        if (PreWork)
            AIControl.Main.SetWork(PreWork);
    }
}
