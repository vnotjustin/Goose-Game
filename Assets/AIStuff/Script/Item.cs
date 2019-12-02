using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public Rigidbody Rig;
    public Collider Col;
    public bool OriAssign;
    [HideInInspector] public Vector3 OriPosition;
    [HideInInspector] public Vector3 OriRotation;
    [Space]
    public GameObject TargetPoint;
    public GameObject HandlePoint;
    [Space]
    public AIWork PickUpWork;
    public AIWork ResetWork;
    [Space]
    public int ActionIndex;
    public bool Interacted;
    public bool Detected;
    public bool Holding;
    public bool HeavyAnim;

    public void Awake()
    {
        if (OriAssign)
        {
            OriPosition = transform.position;
            OriRotation = transform.eulerAngles;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        PositionUpdate();
        if (Interacted && !Holding && !Detected)
        {
            if (AIBehaviourControl.Main.InRange(this))
                OnDetect();
        }
    }

    public void FixedUpdate()
    {
        PositionUpdate();
    }

    public void PositionUpdate()
    {
        if (Holding)
        {
            Vector3 HandlePointChange = transform.position - HandlePoint.transform.position;
            transform.position = AIControl.Main.HandlePoint.transform.position + HandlePointChange;
            transform.eulerAngles = AIControl.Main.HandlePoint.transform.eulerAngles;
        }
    }

    public void ItemReset()
    {
        Rig.velocity = new Vector3();
        Rig.angularVelocity = new Vector3();
        transform.position = OriPosition;
        transform.eulerAngles = OriRotation;
        Interacted = false;
        Detected = false;
        ItemDrop();
        AIBehaviourControl.Main.OnReset(this);
    }

    public void ItemDrop()
    {
        Rig.isKinematic = false;
        Col.isTrigger = false;
        Holding = false;
    }

    public void OnPickUp()
    {
        Rig.isKinematic = true;
        Col.isTrigger = true;
        Holding = true;
        Interacted = true;
        AIBehaviourControl.Main.OnInteract(this);
    }

    public void OnDetect()
    {
        Detected = true;
        AIBehaviourControl.Main.OnDetected(this);
    }

    public void Interact()
    {
        Interacted = true;
        AIBehaviourControl.Main.OnInteract(this);
    }
}
