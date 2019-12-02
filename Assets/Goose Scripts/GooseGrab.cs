using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseGrab : MonoBehaviour
{
    public bool isHolding;
    private GameObject hold;
    public Transform mouth;
    int t;
    int t2;
    // Start is called before the first frame update
    void Start()
    {
        
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
            isHolding = false;
            t2 = 5;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(1) && other.tag == "grabable" && t2 <= 0)
        {
            hold = other.gameObject;
            other.transform.position = mouth.position;
            Debug.Log("colliderworks");
            isHolding = true;
            Debug.Log("holdingworks");
            t = 5;
        }
        else
        {
            Debug.Log("conditions not true");
        }

    }
}
