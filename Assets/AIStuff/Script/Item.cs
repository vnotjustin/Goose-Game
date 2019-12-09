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
    public bool TriggerType;
    public int ActionIndex;
    public bool Resetted;
    public bool Interacted;
    public bool Detected;
    public bool Holding;
    public bool HeavyAnim;
    public bool TriggerLock;
    public bool KinematicLock;

    public void Awake()
    {
        if (OriAssign)
        {
            OriPosition = transform.position;
            OriRotation = transform.eulerAngles;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        PositionUpdate();
        ColliderUpdate();
        if (Interacted && !Holding && !Detected)
        {
            if (AIBehaviourControl.Main.InRange(this))
                OnDetect();
        }
    }

    public void FixedUpdate()
    {
        PositionUpdate();
        ColliderUpdate();
    }

    public virtual void PositionUpdate()
    {
        if (Holding)
        {
            Vector3 HandlePointChange = transform.position - HandlePoint.transform.position;
            transform.position = AIControl.Main.HandlePoint.transform.position + HandlePointChange;
            transform.eulerAngles = AIControl.Main.HandlePoint.transform.eulerAngles;
        }
    }

    public void ColliderUpdate()
    {
        Col.isTrigger = GetTrigger();
        Rig.isKinematic = GetKinematic();
    }

    public virtual void ItemReset()
    {
        Rig.velocity = new Vector3();
        Rig.angularVelocity = new Vector3();
        transform.position = OriPosition;
        transform.eulerAngles = OriRotation;
        Resetted = true;
        Interacted = false;
        Detected = false;
        ItemDrop();
        if (TriggerType)
        {
            TriggerLock = true;
            Rig.useGravity = false;
        }
        AIBehaviourControl.Main.OnReset(this);
    }

    public void ItemDrop()
    {
        KinematicLock = false;
        TriggerLock = false;
        Rig.useGravity = true;
        Holding = false;
    }

    public virtual void OnPickUp()
    {
        Resetted = false;
        KinematicLock = true;
        TriggerLock = true;
        Holding = true;
        Interacted = true;
        AIBehaviourControl.Main.OnInteract(this);
    }

    public void OnDetect()
    {
        Detected = true;
        AIBehaviourControl.Main.OnDetected(this);
    }

    public virtual void Interact()
    {
        Resetted = false;
        Interacted = true;
        Rig.useGravity = true;
        TriggerLock = false;
        AIBehaviourControl.Main.OnInteract(this);
    }

    public virtual bool GetTrigger()
    {
        return TriggerLock;
    }

    public virtual bool GetKinematic()
    {
        return KinematicLock;
    }
}
