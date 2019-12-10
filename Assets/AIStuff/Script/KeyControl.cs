using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour {
    [HideInInspector]
    public static KeyControl Main;
    public bool HaveKey;

    private void Awake()
    {
        Main = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
