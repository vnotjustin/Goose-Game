using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatControl : MonoBehaviour {
    [HideInInspector]
    public static HatControl Main;
    public Item_Hat HatI;
    public Item_Hat HatII;
    public Item_Hat CurrentHat;
    public bool HaveHat;
    public bool AlreadyChangedHat;
    public AIWork ChangeHatWork;
    public float LostHatDelay = 10f;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHat = HatI;
    }

    // Update is called once per frame
    void Update()
    {
        if (HaveHat && (!AIControl.Main.CurrentWork || AIControl.Main.CurrentWork.TargetItem != HatI))
            LostHatDelay = 10f;
        else
            LostHatDelay -= Time.deltaTime;

        if (LostHatDelay < 0 && !AlreadyChangedHat)
        {
            AIControl.Main.SetWork(ChangeHatWork);
            AlreadyChangedHat = true;
            CurrentHat = HatII;
            AIBehaviourControl.Main.OnReset(HatI);
        }
    }
}
