using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseGrab : MonoBehaviour
{
    public bool isHolding;
    private GameObject hold;
    public Transform mouth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding == true)
        {
            hold.transform.position = mouth.transform.position;
        }

        if (Input.GetMouseButtonDown(1) && isHolding == true)
        {
            isHolding = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(1) && other.tag == "grabable")
        {
            hold = other.gameObject;
            other.transform.position = mouth.position;
            Debug.Log("colliderworks");

               isHolding = true;
               Debug.Log("holdingworks");
        }
        else
        {
            Debug.Log("conditions not true");
        }

    }
}
