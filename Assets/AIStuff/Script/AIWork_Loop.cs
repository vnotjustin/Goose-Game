using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWork_Loop : AIWork {
    [Space]
    public AIWork NextWork;

    public override void OnEnd()
    {
        base.OnEnd();
        AIControl.Main.SetWork(NextWork);
    }
}
