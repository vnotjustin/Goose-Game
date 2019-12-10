using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourControl : MonoBehaviour {
    [HideInInspector]
    public static AIBehaviourControl Main;
    public float Range;
    public List<Item> Items;
    public int ActionIndex;
    [Space]
    public List<AIWork> RandomWorks;
    public AIWork LastRandomWork;
    public float MaxWaitTime;
    public float WaitTime;
    public float ProtectedTime;
    [Space]
    public float HatWorkRate = 0.3f;
    public bool LastHatWork;
    public AIWork HatWork;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AIControl.Main.CurrentWork)
        {
            WaitTime = MaxWaitTime;
        }
        else
        {
            ProtectedTime -= Time.deltaTime;
            WaitTime -= Time.deltaTime;
        }

        if (WaitTime <= 0)
        {
            if (!LastHatWork && Random.Range(0, 1f) <= HatWorkRate)
            {
                AIControl.Main.SetWork(HatWork);
                LastHatWork = true;
            }
            else
            {
                LastHatWork = false;
                List<AIWork> W = new List<AIWork>();
                foreach (AIWork AW in RandomWorks)
                    W.Add(AW);
                if (W.Contains(LastRandomWork))
                    W.Remove(LastRandomWork);
                if (W.Count > 0)
                {
                    AIWork AW = W[Random.Range(0, W.Count)];
                    AIControl.Main.SetWork(AW);
                    LastRandomWork = AW;
                    WaitTime = MaxWaitTime;
                }
            }
        }

        if ((!AIControl.Main.CurrentWork || AIControl.Main.CurrentWork.Interrupted) && GetNextItem() && ProtectedTime < 0)
        {
            AIControl.Main.SetWork(GetNextItem().PickUpWork);
        }
    }

    public void DropItem()
    {
        if (!AIControl.Main.CurrentItem)
            return;
        AIControl.Main.CurrentItem.ItemDrop();
    }

    public void OnInteract(Item I)
    {
        ActionIndex++;
        I.ActionIndex = ActionIndex;
    }

    public void OnReset(Item I)
    {
        if (Items.Contains(I))
            Items.Remove(I);
    }

    public void OnPickUp(Item I)
    {

    }

    public void OnDetected(Item I)
    {
        if (!Items.Contains(I) && !I.Ignored)
            Items.Add(I);
    }

    public bool InRange(Item I)
    {
        Vector3 i = I.transform.position;
        Vector3 a = transform.position;
        i = new Vector3(i.x, a.y, i.z);
        return PathObstacle.GetDistance(a, i) < Range;
    }

    public Item GetNextItem()
    {
        if (Items.Count <= 0)
            return null;
        int a = -1;
        Item I = null;
        for (int i = Items.Count - 1; i >= 0; i--)
        {
            if (Items[i].ActionIndex > a)
            {
                I = Items[i];
                a = Items[i].ActionIndex;
            }
        }
        return I;
    }
}
