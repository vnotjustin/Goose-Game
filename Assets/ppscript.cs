using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ppscript : MonoBehaviour
{
    public PostProcessVolume ppvol;
    // Start is called before the first frame update
    void Start()
    {
        ppvol.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gooseControl.pptrue)
        {
            ppvol.weight = 1;
        }
        else if (!gooseControl.pptrue)
        {
            ppvol.weight = 0;
        }
    }
}
