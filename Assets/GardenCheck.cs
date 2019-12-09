using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenCheck : MonoBehaviour
{
    public GameObject goose;

    TaskCheck tc;

    void Start()
    {
        GameObject TaskChecker = GameObject.FindWithTag("Checker");
        tc = TaskChecker.GetComponent<TaskCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == goose)
        {
            tc.getin = true;
        }

    }
    }
