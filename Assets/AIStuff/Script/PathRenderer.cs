using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour {
    public int Index;
    public GameObject AnimBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AIControl.Main.Path.corners.Length > Index)
        {
            transform.position = AIControl.Main.Path.corners[Index];
            AnimBase.SetActive(true);
        }
        else
            AnimBase.SetActive(false);
    }
}
