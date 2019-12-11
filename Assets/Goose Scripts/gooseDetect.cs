using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gooseDetect : MonoBehaviour
{
    public bool inRange = false;
    public GameObject honk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gooseControl.goose.transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject G = Instantiate(honk);
            G.transform.position = transform.position;
            if (inRange == true)
            {
                AIControl.Main.Heard("goose");
            }

        }

        if (!inRange)
        {
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
