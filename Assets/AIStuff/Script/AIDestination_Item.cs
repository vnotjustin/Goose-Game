using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIDestination_Item : AIDestination {
    public Item I;
    public float Range;
    public bool Reset;
    public List<Vector3> AlterPositions;

    public void Awake()
    {
        AlterPositionIni();
    }

    public void AlterPositionIni()
    {
        AlterPositions = new List<Vector3>();
        AlterPositions.Add(new Vector3(-1, 0, -1).normalized * Range);
        AlterPositions.Add(new Vector3(-1, 0, 1).normalized * Range);
        AlterPositions.Add(new Vector3(1, 0, -1).normalized * Range);
        AlterPositions.Add(new Vector3(1, 0, 1).normalized * Range);
        AlterPositions.Add(new Vector3(-1f, 0, -1.73f).normalized * Range);
        AlterPositions.Add(new Vector3(-1.73f, 0, -1f).normalized * Range);
        AlterPositions.Add(new Vector3(-1f, 0, 1.73f).normalized * Range);
        AlterPositions.Add(new Vector3(-1.73f, 0, 1f).normalized * Range);
        AlterPositions.Add(new Vector3(1f, 0, -1.73f).normalized * Range);
        AlterPositions.Add(new Vector3(1.73f, 0, -1f).normalized * Range);
        AlterPositions.Add(new Vector3(1f, 0, 1.73f).normalized * Range);
        AlterPositions.Add(new Vector3(1.73f, 0, 1f).normalized * Range);
    }

    public override Vector3 GetTargetPosition()
    {
        Vector3 i = I.transform.position;
        if (Reset)
            i = I.OriPosition;
        Vector3 a = AIControl.Main.GetPosition();
        i = new Vector3(i.x, a.y, i.z);
        if (PathObstacle.GetDistance(i, a) <= Range)
            return a;
        int Index = -1;
        Vector3 Temp = i + (a - i).normalized * Range;
        while (!PathCheck(Temp) && Index < AlterPositions.Count - 1)
        {
            Index++;
            Temp = AlterPositions[Index] + i;
        }
        return Temp;
    }

    public bool PathCheck(Vector3 Position)
    {
        NavMeshPath P = new NavMeshPath();
        AIControl.Main.Agent.CalculatePath(Position, P);
        return P.status == NavMeshPathStatus.PathComplete;
    }

    public override bool OutRange()
    {
        return base.OutRange();
    }
}
