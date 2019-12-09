using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeCheck : MonoBehaviour
{
    public GameObject groundkeeper;
    public GameObject rake;

    
    // Start is called before the first frame update
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

        if(other.gameObject == rake)
        {
            tc.ralake = true;
        }
        if (other.gameObject == groundkeeper)
        {
            tc.gkwet = true;
        }
    }
}
