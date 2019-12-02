using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseGrab : MonoBehaviour
{
    public bool isHolding;
    private GameObject hold;
    public Transform mouth;
    bool callFirst;
    int t;
    int t2;
    // Start is called before the first frame update
    void Start()
    {
        callFirst = true;
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
            hold.transform.position = mouth.transform.position;
        }

        if (Input.GetMouseButtonDown(1) && isHolding == true && t <= 0)
        {
            Debug.Log("dropped");
            isHolding = false;
            t2 = 5;
            callFirst = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(1) && other.tag == "grabable" && t2 <= 0 && callFirst == true)
        {
            callFirst = false;
            hold = other.gameObject;
            other.transform.position = mouth.position;;
            isHolding = true;
            Debug.Log("holdingworks");
            t = 5;
        }

    }
}
