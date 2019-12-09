using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatControl : MonoBehaviour {
    [HideInInspector]
    public static HatControl Main;
    public Item_Hat HatI;
    public Item_Hat HatII;
    public bool HaveHat;
    public bool AlreadyChangedHat;
    public AIWork ChangeHatWork;
    public float LostHatDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HaveHat)
            LostHatDelay = 3f;
        else
            LostHatDelay -= Time.deltaTime;

        if (LostHatDelay < 0 && !AlreadyChangedHat)
        {
            AIControl.Main.SetWork(ChangeHatWork);
            AlreadyChangedHat = true;
            AIBehaviourControl.Main.OnReset(HatI);
        }
    }
}
