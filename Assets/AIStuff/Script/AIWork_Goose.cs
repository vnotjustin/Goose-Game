using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWork_Goose : AIWork {
    [Space]
    public AIWork OriWork;

    public override void OnStart(AIWork LastWork)
    {
        OriWork = LastWork;
        AIBehaviourControl.Main.DropItem();
        /*
        if (gooseControl.goose.GetComponentInChildren<GooseGrab>().hold)
            gooseControl.goose.GetComponentInChildren<GooseGrab>().hold.GetComponent<Item>().OnDetect();*/
    }

    public override void OnEnd()
    {
        base.OnEnd();
        //AIControl.Main.SetWork(OriWork);
    }

    public override Vector3 GetTargetPosition()
    {
        Vector3 G = gooseControl.goose.transform.position;
        G.y = AIControl.Main.GetPosition().y;
        return AIControl.Main.GetPosition() + (G - AIControl.Main.GetPosition()).normalized * 0.095f;
    }
}
