using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDestination_Item : AIDestination {
    public Item I;
    public float Range;

    public override Vector3 GetTargetPosition()
    {
        Vector3 i = I.transform.position;
        Vector3 a = AIControl.Main.transform.position;
        i = new Vector3(i.x, a.y, i.z);
        if (PathObstacle.GetDistance(i, a) <= Range)
            return a;
        return a + (i - a).normalized * Range;
    }
}
