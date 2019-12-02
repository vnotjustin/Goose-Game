using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDestination_Item : AIDestination {
    public Item I;
    public float Range;
    public bool Reset;

    public override Vector3 GetTargetPosition()
    {
        Vector3 i = I.transform.position;
        if (Reset)
            i = I.OriPosition;
        Vector3 a = AIControl.Main.GetPosition();
        i = new Vector3(i.x, a.y, i.z);
        if (PathObstacle.GetDistance(i, a) <= Range)
            return a;
        return a + (i - a).normalized * Range;
    }

    public override bool OutRange()
    {
        return base.OutRange();
    }
}
