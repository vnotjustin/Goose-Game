using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDestination_Goose : AIDestination
{

    public override Vector3 GetTargetPosition()
    {
        return (gooseControl.goose.transform.position - AIControl.Main.GetPosition()).normalized * 0.05f + AIControl.Main.GetPosition();
    }
}
