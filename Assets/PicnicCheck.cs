using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicnicCheck : MonoBehaviour
{
    public GameObject sandwich;
    public GameObject apple;
    public GameObject[] pumpkins;
    public GameObject[] carrots;
    public GameObject jam;
    public GameObject thermos;
    public GameObject radio;
    public GameObject basket;
    public bool sc = false;
    public bool ac = false;
    public bool pc = false;
    public bool cc = false;
    public bool jc = false;
    public bool thc = false;
    public bool rc = false;
    public bool bc = false;
    TaskCheck tc;

    void Start()
    {
        GameObject TaskChecker = GameObject.FindWithTag("Checker");
        tc = TaskChecker.GetComponent<TaskCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sc && ac && pc && cc && jc & thc && rc && bc)
        {
            tc.picnic = true;
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == sandwich)
        {
            sc = true;
        }
        if (other.gameObject == apple)
        {
            ac = true;
        }
        if (other.gameObject == pumpkins[0] || other.gameObject == pumpkins[1] || other.gameObject == pumpkins[2] || other.gameObject == pumpkins[3] || other.gameObject == pumpkins[4] || other.gameObject == pumpkins[5] || other.gameObject == pumpkins[6] || other.gameObject == pumpkins[7] || other.gameObject == pumpkins[8] || other.gameObject == pumpkins[9])
        {
            pc = true;
        }
        if (other.gameObject == carrots[0] || other.gameObject == carrots[1] || other.gameObject == carrots[2] || other.gameObject == carrots[3] || other.gameObject == carrots[4] || other.gameObject == carrots[5] || other.gameObject == carrots[6] || other.gameObject == carrots[7] || other.gameObject == carrots[8] || other.gameObject == carrots[9] || other.gameObject == carrots[10])
        {
            cc = true;
        }
        if (other.gameObject == jam)
        {
            jc = true;
        }
        if (other.gameObject == thermos)
        {
            thc = true;
        }
        if (other.gameObject == radio)
        {
            rc = true;
        }
        if (other.gameObject == basket)
        {
            bc = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == sandwich)
        {
            sc = false;
        }
        if (other.gameObject == apple)
        {
            ac = false;
        }
        if (other.gameObject == pumpkins[0] || other.gameObject == pumpkins[1] || other.gameObject == pumpkins[2] || other.gameObject == pumpkins[3] || other.gameObject == pumpkins[4] || other.gameObject == pumpkins[5] || other.gameObject == pumpkins[6] || other.gameObject == pumpkins[7] || other.gameObject == pumpkins[8] || other.gameObject == pumpkins[9])
        {
            pc = false;
        }
        if (other.gameObject == carrots[0] || other.gameObject == carrots[1] || other.gameObject == carrots[2] || other.gameObject == carrots[3] || other.gameObject == carrots[4] || other.gameObject == carrots[5] || other.gameObject == carrots[6] || other.gameObject == carrots[7] || other.gameObject == carrots[8] || other.gameObject == carrots[9] || other.gameObject == carrots[10])
        {
            cc = false;
        }
        if (other.gameObject == jam)
        {
            jc = false;
        }
        if (other.gameObject == thermos)
        {
            thc = false;
        }
        if (other.gameObject == radio)
        {
            rc = false;
        }
        if (other.gameObject == basket)
        {
            bc = false;
        }

    }
}
