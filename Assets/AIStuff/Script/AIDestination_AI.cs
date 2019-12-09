using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDestination_AI : AIDestination {

    public override Vector3 GetTargetPosition()
    {
        return AIControl.Main.GetPosition();
    }
}
