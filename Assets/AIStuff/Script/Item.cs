using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public Rigidbody Rig;
    public Collider Col;
    [HideInInspector] public Vector3 OriPosition;
    [HideInInspector] public Vector3 OriRotation;
    [Space]
    public GameObject TargetPoint;
    public GameObject HandlePoint;
    [Space]
    public AIWork PickUpWork;
    public AIWork ResetWork;
    [Space]
    public bool Holding;

    public void Awake()
    {
        OriPosition = transform.position;
        OriRotation = transform.eulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        PositionUpdate();
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
        ItemDrop();
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
    }
}
