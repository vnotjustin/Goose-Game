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

    void Start()
    {
        tc = GetComponent<TaskCheck>(); //getting the TaskCheck script 
    }
            
    void Update()
    {
        
    }

    public void CheckingTasks() //function for checking the tasks to play animation 
    {
        if (tc.getin == true && getina == false)
        {
            //play animation 
        }
        else
        {
            //don't do anything
        }
    }
}
