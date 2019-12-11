using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeCheck : MonoBehaviour
{
    public GameObject groundkeeper;
    public GameObject rake;
    public GameObject Radio;
    
    // Start is called before the first frame update
    TaskCheck tc;

    void Start()
    {
        GameObject TaskChecker = GameObject.FindWithTag("Checker");
        tc = TaskChecker.GetComponent<TaskCheck>();
        Radio = FindObjectOfType<Item_Radio>().gameObject;
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
        if (other.gameObject == Radio)
            Radio.GetComponent<Item_Radio>().Broken = true;
    }
}
