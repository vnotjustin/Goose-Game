using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Hat : Item {
    public List<ItemPoint> Points;
    public bool StartDisable;

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
        if (StartDisable || !HatControl.Main.HaveHat)
            return null;
        foreach (ItemPoint P in Points)
            if (P.Active)
                return P;
        return null;
    }

    public override void Interact()
    {
        base.Interact();
        HatControl.Main.HaveHat = false;
    }

    public override void ItemReset()
    {
        base.ItemReset();
        StartDisable = false;
        HatControl.Main.HaveHat = true;
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
        StartDisable = false;
    }

    public override bool GetKinematic()
    {
        if (StartDisable)
            return true;
        if (GetPoint())
            return GetPoint().Kinematic;
        return base.GetKinematic();
    }
}
