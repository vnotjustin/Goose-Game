using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWork_Hat : AIWork {
    [Space]
    public bool Active;

    public override void Update()
    {
        base.Update();
        /*if (Active && !HatControl.Main.HaveHat)
            AIControl.Main.EndWork();*/
    }

    public override void OnStart(AIWork LastWork)
    {
        base.OnStart(LastWork);
        Active = true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override Vector3 GetTargetPosition()
    {
        return base.GetTargetPosition();
    }
}
