﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enableui : MonoBehaviour
{
    public GameObject todo;
    // Start is called before the first frame update
    void Start()
    {
        todo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gooseControl.pptrue)
        {
            todo.SetActive(true);
        }
        else if (!gooseControl.pptrue)
        {
            todo.SetActive(false);
        }
    }
}
