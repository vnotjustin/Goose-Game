using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : Item {
    public List<ItemPoint> Points;

    public override void PositionUpdate()
    {
        if (GetPoint())
        {
            transform.position = GetPoint().transform.position;
            transform.eulerAngles = GetPoint().transform.eulerAngles;
        }
        else
            base.PositionUpdate();
    }

    public ItemPoint GetPoint()
    {
        if (!KeyControl.Main.HaveKey)
            return null;
        foreach (ItemPoint P in Points)
            if (P.Active)
                return P;
        return null;
    }

    public override void Interact()
    {
        base.Interact();
        KeyControl.Main.HaveKey = false;
    }

    public override void ItemReset()
    {
        base.ItemReset();
        KeyControl.Main.HaveKey = true;
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
    }

    public override bool GetKinematic()
    {
        if (GetPoint())
            return GetPoint().Kinematic;
        return base.GetKinematic();
    }
}
