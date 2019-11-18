using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDestination : MonoBehaviour {
    public GameObject TargetPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetTargetPosition()
    {
        if (TargetPoint)
            return TargetPoint.transform.position;
        return transform.position;
    }
}
