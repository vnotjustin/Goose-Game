using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAI : MonoBehaviour
{
    TaskCheck tc;
    public bool getina = false; //bool for preventing animation from always replaying once player presses tab
    public bool gkweta = false;
    public bool stkeya = false;
    public bool wehata = false;
    public bool ralakea = false;
    public bool picnica = false;

    public Animator lineAnimation;
    public Animator lineAnimation2;
    public Animator lineAnimation3;

    void Start()
    {
        tc = GetComponent<TaskCheck>(); //getting the TaskCheck script 
    }
            
    void Update()
    {
        CheckingTasks();
    }

    public void CheckingTasks() //function for checking the tasks to play animation 
    {
        if (tc.getin == true && getina == false/* && Input.GetKey(KeyCode.Tab)*/)
        {
            lineAnimation.SetBool("getina", true); //play animation
            Debug.Log("playing strike");
            getina = true; //prevents player from seeing the strikethrough animation every time they press tab
        }

        if(tc.gkwet == true && gkweta == false)
        {
            lineAnimation2.SetBool("gkweta", true);
            Debug.Log("strike2");
            gkweta = true;
        }

        if (tc.stkey == true && stkeya == false)
        {
            lineAnimation3.SetBool("stkeya", true);
            stkeya = true;
        }

        if (tc.wehat == true && wehata == false)
        {
            wehata = true; 
            //play animation
        }

        if (tc.ralake == true && ralakea == false)
        {
            ralakea = true; 
            //play animation
        }

        if (tc.picnic == true && picnica == false)
        {
            picnica = true; 
            //play animation
        }

    }
}
