using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseGrab : MonoBehaviour
{
    public bool isHolding;
    public GameObject hold;
    public Transform mouth;
    bool callFirst;
    Collider hbx;
    Collider hbic;
    int t;
    int t2;
    Item it;
    gooseControl gc;
    public GameObject keys;
    TaskCheck tc;
    // Start is called before the first frame update
    void Start()
    {
        callFirst = true;
        gc = GetComponent<gooseControl>();
        GameObject TaskChecker = GameObject.FindWithTag("Checker");
        tc = TaskChecker.GetComponent<TaskCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (t > 0)
        {
            t--;
        }
        if (t2 > 0)
        {
            t2--;
        }
        if (isHolding == true)
        {
            if(hold.gameObject == keys)
            {
                tc.stkey = true;
            }
            hold.transform.position = mouth.transform.position;
            hbx = hold.GetComponent<Collider>();
            hbx.enabled = false;
            hbic = hold.GetComponentInChildren<Collider>();
            hbic.enabled = false;

        }

        if (Input.GetMouseButtonDown(1) && isHolding == true && t <= 0)
        {
            hbx.enabled = true;
            hbic.enabled = true;
            Debug.Log("dropped");
            isHolding = false;
            t2 = 5;
            callFirst = true;
        }
    }

    public void Grabbed(GameObject item)
    {
        if(isHolding == true && t <= 0 && item == hold)
        {
            hbx.enabled = true;
            hbic.enabled = true;
            isHolding = false;
            t2 = 5;
            callFirst = true;
            //gc.RunOut();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(1) && other.tag == "grabable" && t2 <= 0 && callFirst == true && (!AIControl.Main.CurrentItem || other.gameObject != AIControl.Main.CurrentItem.gameObject))
        {
            callFirst = false;
            hold = other.gameObject;
            other.transform.position = mouth.position;;
            isHolding = true;
            Debug.Log("holdingworks");
            t = 5;
            other.GetComponent<Item>().Interact();
        }

    }
}
