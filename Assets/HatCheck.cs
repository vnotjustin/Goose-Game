using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatCheck : MonoBehaviour
{
    public GameObject ai;

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

        if (other.gameObject == ai)
        {
            tc.wehat = true;
        }

    }
}
