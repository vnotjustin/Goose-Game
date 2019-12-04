using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wingFlap : MonoBehaviour
{
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }
      

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            m_Animator.SetBool("isFlap", true);
        }
        else
        {
            m_Animator.SetBool("isFlap", false);
        }
    }
}
